﻿using System;
using System.Collections.Generic;
using System.Linq;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Integration.AmoCRM;
using Hopnscotch.Portal.Integration.AmoCRM.DataProvider;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Import
{
    public sealed class AmoCrmImportManager : IAmoCrmImportManager
    {
        // temporary const for generating lesson stubs
        private const int NumberOfLessons = 8;
        
        private IAmoDataProvider amoDataProvider;
        private readonly IAttendanceUow attendanceUow;
        private readonly IAmoCrmEntityConverter entityConverter;

        public AmoCrmImportManager(IAmoDataProvider amoDataProvider, IAttendanceUow attendanceUow, IAmoCrmEntityConverter entityConverter)
        {
            this.amoDataProvider = amoDataProvider;
            this.attendanceUow = attendanceUow;
            this.entityConverter = entityConverter;
        }

        public AmoCrmImportResult Import(AmoCrmImportOptions options)
        {
            if (options.SimulateImport)
            {
                amoDataProvider = new SimulationImportDataProvider(attendanceUow);
            }

            amoDataProvider.SaveDataDuringImport = options.SaveImportData;

            if (!amoDataProvider.Authenticate())
            {
                return new AmoCrmImportResult(new []
                {
                    new AmoCrmImportResultError
                    {
                        EntityType = AmoCrmEntityTypes.None,
                        Message = "Could not authenticate in amoCRM API"
                    }
                });
            }

            if (options.StartFromScratch)
            {
                ClearExistingAttendanceData();
            }

            AmoCrmImportContext context;
            try
            {
                context = new AmoCrmImportContext(amoDataProvider, entityConverter);
            }
            catch (ImportSimulationException e)
            {
                return new AmoCrmImportResult(new[]
                {
                    new AmoCrmImportResultError
                    {
                        EntityType = AmoCrmEntityTypes.None,
                        Message = e.Message
                    }
                });
            }
            
            ImportUsers(context);
            ImportLevels(context);
            SetupContactLeadLinks(context);
            ImportContacts(context);
            ImportLeads(context);

            attendanceUow.Commit();

            return new AmoCrmImportResult();
        }

        private void ImportLeads(AmoCrmImportContext context)
        {
            foreach (var lead in context.LeadsMap.Values)
            {
                // set responsible user
                User user;
                if (context.UsersMap.TryGetValue(lead.AmoResponsibleUserId, out user))
                {
                    lead.ResponsibleUser = user;
                }

                // set group level if set and exists
                if (lead.AmoLevelId.HasValue)
                {
                    lead.LanguageLevel = attendanceUow.Levels.GetByAmoId(lead.AmoLevelId.Value);
                }
                
                var existingLead = attendanceUow.Leads.GetByAmoId(lead.AmoId);
                if (existingLead == null)
                {
                    // generate lessons according to schedule and add them to datacontext
                    foreach (var lesson in CreateLessonsForLead(lead))
                    {
                        // generate default attendance records
                        foreach (var contact in lead.Contacts)
                        {
                            var attendance = new Attendance
                            {
                                Attended = false,
                                Contact = contact,
                                Lesson = lesson
                            };

                            lesson.Attendances.Add(attendance);
                            attendanceUow.Attendances.Add(attendance);
                        }

                        attendanceUow.Lessons.Add(lesson);
                    }

                    attendanceUow.Leads.Add(lead);
                }
                else
                {
                    existingLead.CopyValuesFrom(lead);
                    attendanceUow.Leads.Update(existingLead);
                }
            }
        }

        private void ImportContacts(AmoCrmImportContext context)
        {
            foreach (var contact in context.ContactsMap.Values)
            {
                // set responsible user
                User user;
                if (context.UsersMap.TryGetValue(contact.AmoResponsibleUserId, out user))
                {
                    contact.ResponsibleUser = user;
                }
                
                var existingContact = attendanceUow.Contacts.GetByAmoId(contact.AmoId);
                if (existingContact == null)
                {
                    attendanceUow.Contacts.Add(contact);
                }
                else
                {
                    existingContact.CopyValuesFrom(contact);
                    attendanceUow.Contacts.Update(existingContact);
                }
            }
        }

        private static void SetupContactLeadLinks(AmoCrmImportContext context)
        {
            foreach (var link in context.ContactLeadLinks)
            {
                Contact contact;
                Lead lead;
                if (context.ContactsMap.TryGetValue(link.ContactId, out contact) && 
                    context.LeadsMap.TryGetValue(link.LeadId, out lead))
                {
                    contact.Leads.Add(lead);
                }
            }
        }

        private void ImportLevels(AmoCrmImportContext context)
        {
            foreach (var level in context.LevelsMap.Values)
            {
                var existingLevel = attendanceUow.Levels.GetByAmoId(level.AmoId);
                if (existingLevel == null)
                {
                    attendanceUow.Levels.Add(level);
                }
                else
                {
                    existingLevel.CopyValuesFrom(level);
                    attendanceUow.Levels.Update(existingLevel);
                }
            }
        }

        private void ImportUsers(AmoCrmImportContext context)
        {
            foreach (var user in context.UsersMap.Values)
            {
                var existingUser = attendanceUow.Users.GetByAmoId(user.AmoId);
                if (existingUser == null)
                {
                    attendanceUow.Users.Add(user);
                }
                else
                {
                    existingUser.CopyValuesFrom(user);
                    attendanceUow.Users.Update(existingUser);
                }
            }
        }

        public void ClearExistingAttendanceData()
        {
            foreach (var attendance in attendanceUow.Attendances.GetAll())
            {
                attendanceUow.Attendances.Delete(attendance);
            }

            foreach (var lesson in attendanceUow.Lessons.GetAll())
            {
                attendanceUow.Lessons.Delete(lesson);
            }
            
            //foreach (var task in attendanceUow.Tasks.GetAll())
            //{
            //    attendanceUow.Tasks.Delete(task);
            //}

            foreach (var lead in attendanceUow.Leads.GetAll())
            {
                lead.Contacts.Clear();
                attendanceUow.Leads.Delete(lead);
            }

            foreach (var contact in attendanceUow.Contacts.GetAll())
            {
                contact.Leads.Clear();
                attendanceUow.Contacts.Delete(contact);
            }
            
            foreach (var level in attendanceUow.Levels.GetAll())
            {
                attendanceUow.Levels.Delete(level);
            }
            
            foreach (var user in attendanceUow.Users.GetAll())
            {
                attendanceUow.Users.Delete(user);
            }

            attendanceUow.Commit();
        }

        private IEnumerable<Lesson> CreateLessonsForLead(Lead lead)
        {
            if (!lead.StartDate.HasValue)
            {
                return Enumerable.Empty<Lesson>();
            }

            // TODO: calculate the exact number based on course length for corresponding language level

            return CalculateLessonDates(lead.StartDate.Value, lead.ScheduleText).Select(lessonDate => new Lesson
            {
                AcademicHours = 3,
                Date = lessonDate,
                Lead = lead
            });
        }

        private IEnumerable<DateTime> CalculateLessonDates(DateTime startDate, string scheduleText)
        {
            // Пн - Ср  09:00-10:30

            var lessonDates = new List<DateTime>();
            var lessonsCreated = 0;
            var date = startDate;
            while (lessonsCreated <= NumberOfLessons)
            {
                if (date.DayOfWeek == DayOfWeek.Monday || date.DayOfWeek == DayOfWeek.Wednesday)
                {
                    lessonsCreated++;
                    lessonDates.Add(date);
                }

                date = date.AddDays(1);
            }

            return lessonDates;
        }

        class Schedule
        {
            DayOfWeek[] Days { get; set; }
        }

        public void Dispose()
        {
            attendanceUow.Dispose();
        }
    }
}
