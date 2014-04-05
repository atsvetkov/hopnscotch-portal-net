using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Integration.AmoCRM.DataProvider;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Import
{
    public sealed class AmoCrmImportManager : IAmoCrmImportManager
    {
        // temporary const for generating lesson stubs
        private const int NumberOfLessons = 8;

        private const string LevelCustomFieldName = "Уровень";

        private readonly IAmoDataProvider _amoDataProvider;
        private readonly IAttendanceUow _attendanceUow;
        private readonly IAmoCrmEntityConverter _entityConverter;

        public AmoCrmImportManager(IAmoDataProvider amoDataProvider, IAttendanceUow attendanceUow, IAmoCrmEntityConverter entityConverter)
        {
            _amoDataProvider = amoDataProvider;
            _attendanceUow = attendanceUow;
            _entityConverter = entityConverter;
        }

        public AmoCrmImportResult Import(AmoCrmImportOptions options)
        {
            //if (!_amoDataProvider.AuthenticateAsync().Result)
            if (!_amoDataProvider.Authenticate())
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

            //var contactsResponse = _amoDataProvider.GetContactsAsync().Result;
            //var leadsResponse = _amoDataProvider.GetLeadsAsync().Result;
            //var contactLeadLinksResponse = _amoDataProvider.GetContactLeadLinksAsync().Result;
            //var accountResponse = _amoDataProvider.GetAccountAsync().Result;

            var contactsResponse = _amoDataProvider.GetContacts();
            var leadsResponse = _amoDataProvider.GetLeads();
            var contactLeadLinksResponse = _amoDataProvider.GetContactLeadLinks();
            var accountResponse = _amoDataProvider.GetAccount();
            
            var contactsMap = contactsResponse.Response.Contacts.Select(r => _entityConverter.Convert(r)).ToDictionary(c => c.AmoId);
            var leadsMap = leadsResponse.Response.Leads.Select(r => _entityConverter.Convert(r)).ToDictionary(c => c.AmoId);
            var usersMap = accountResponse.Response.Account.Users.Select(u => _entityConverter.Convert(u)).ToDictionary(c => c.AmoId);
            var contactLeadLinks = contactLeadLinksResponse.Response.Links;
            
            var levelsCustomField = accountResponse.Response.Account.CustomFields.LeadFields.FirstOrDefault(f => f.Name == LevelCustomFieldName);
            var levelsMap = new Dictionary<int, Level>();
            if (levelsCustomField != null)
            {
                levelsMap = _entityConverter.Convert(levelsCustomField).ToDictionary(l => l.AmoId);
            }

            // add users to datacontext
            foreach (var user in usersMap.Values)
            {
                _attendanceUow.Users.Add(user);
            }

            // add levels to datacontext
            foreach (var level in levelsMap.Values)
            {
                _attendanceUow.Levels.Add(level);
            }

            // setup contact-to-lead relationships
            foreach (var link in contactLeadLinks)
            {
                Contact contact;
                Lead lead;
                if (contactsMap.TryGetValue(link.ContactId, out contact) && leadsMap.TryGetValue(link.LeadId, out lead))
                {
                    contact.Leads.Add(lead);
                }
            }

            // set related entities for contacts and add contacts to datacontext
            foreach (var contact in contactsMap.Values)
            {
                // set responsible user
                User user;
                if (usersMap.TryGetValue(contact.AmoResponsibleUserId, out user))
                {
                    contact.ResponsibleUser = user;
                }

                _attendanceUow.Contacts.Add(contact);
            }

            // set related entities for leads and add leads to datacontext
            foreach (var lead in leadsMap.Values)
            {
                // set responsible user
                User user;
                if (usersMap.TryGetValue(lead.AmoResponsibleUserId, out user))
                {
                    lead.ResponsibleUser = user;
                }

                // set group level if set and exists
                Level level;
                if (lead.AmoLevelId.HasValue && levelsMap.TryGetValue(lead.AmoLevelId.Value, out level))
                {
                    lead.LanguageLevel = level;
                }

                // generate lessons according to schedule and add them to datacontext
                foreach (var lesson in CreateLessonsForLead(lead))
                {
                    _attendanceUow.Lessons.Add(lesson);
                }

                _attendanceUow.Leads.Add(lead);
            }

            _attendanceUow.Commit();

            return new AmoCrmImportResult();
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
            _attendanceUow.Dispose();
        }
    }
}
