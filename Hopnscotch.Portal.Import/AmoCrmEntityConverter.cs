using System;
using System.Collections.Generic;
using System.Linq;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Import
{
    public sealed class AmoCrmEntityConverter : IAmoCrmEntityConverter
    {
        private const string AmoMondayName = "Пн";
        private const string AmoTuesdayName = "Вт";
        private const string AmoWednesdayName = "Ср";
        private const string AmoThursdayName = "Чт";
        private const string AmoFridayName = "Пт";
        private const string AmoSaturdayName = "Сб";
        private const string AmoSundayName = "Вс";

        public Contact Convert(ApiIndividualContactResponse response)
        {
            return new Contact
            {
                AmoId = response.Id,
                AmoResponsibleUserId = response.ResponsibleUserId,
                Created = GetDateTimeOrDefault(response.Created),
                Modified = GetDateTimeOrDefault(response.LastModified),
                Name = response.Name,
            };
        }

        public Lead Convert(ApiLeadResponse response)
        {
            return new Lead
            {
                AmoId = response.Id,
                AmoLevelId = response.AmoLevelId,
                AmoStatusId = response.StatusId,
                AmoResponsibleUserId = response.ResponsibleUserId,
                Name = response.Name,
                Created = GetDateTimeOrDefault(response.Created),
                Modified = GetDateTimeOrDefault(response.LastModified),
                Price = response.Price.GetValueOrDefault(),
                ScheduleText = response.ScheduleText,
                StartDate = response.StartDate,
                Duration = response.Duration,
                Days = ConvertWeekDays(response.Days)
            };
        }
        
        public User Convert(ApiUserResponse response)
        {
            return new User
            {
                AmoId = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                Login = response.Login
            };
        }

        public LeadStatus Convert(ApiLeadStatusResponse response)
        {
            return new LeadStatus
            {
                AmoId = response.Id,
                Name = response.Name,
                Color = response.Color
            };
        }

        public Task Convert(ApiTaskResponse response)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Convert<T>(ApiCustomFieldDefinitionResponse response) where T : AmoNamedEntityBase, new()
        {
            if (response == null || response.Enums == null)
            {
                yield break;
            }

            foreach (var fieldValue in response.Enums)
            {
                yield return new T
                {
                    AmoId = fieldValue.Key,
                    Name = fieldValue.Value
                };
            }
        }

        private DateTime? GetDateTimeOrDefault(DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ? default(DateTime?) : dateTime;
        }

        private static DayOfWeek[] ConvertWeekDays(ICollection<string> days)
        {
            if (days == null || days.Count == 0)
            {
                return null;
            }

            var daysOfWeek = new HashSet<DayOfWeek>();
            foreach (var day in days)
            {
                switch (day)
                {
                    case AmoMondayName:
                        daysOfWeek.Add(DayOfWeek.Monday);
                        break;
                    case AmoTuesdayName:
                        daysOfWeek.Add(DayOfWeek.Tuesday);
                        break;
                    case AmoWednesdayName:
                        daysOfWeek.Add(DayOfWeek.Wednesday);
                        break;
                    case AmoThursdayName:
                        daysOfWeek.Add(DayOfWeek.Thursday);
                        break;
                    case AmoFridayName:
                        daysOfWeek.Add(DayOfWeek.Friday);
                        break;
                    case AmoSaturdayName:
                        daysOfWeek.Add(DayOfWeek.Saturday);
                        break;
                    case AmoSundayName:
                        daysOfWeek.Add(DayOfWeek.Sunday);
                        break;
                }
            }

            return daysOfWeek.ToArray();
        }
    }
}