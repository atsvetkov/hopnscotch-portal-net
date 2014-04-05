using System;
using System.Collections.Generic;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Import
{
    public sealed class AmoCrmEntityConverter : IAmoCrmEntityConverter
    {
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
                AmoResponsibleUserId = response.ResponsibleUserId,
                Name = response.Name,
                Created = GetDateTimeOrDefault(response.Created),
                Modified = GetDateTimeOrDefault(response.LastModified),
                Price = response.Price.GetValueOrDefault(),
                ScheduleText = response.ScheduleText,
                StartDate = response.StartDate
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

        public Task Convert(ApiTaskResponse response)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Level> Convert(ApiCustomFieldDefinitionResponse response)
        {
            if (response == null || response.Enums == null)
            {
                yield break;
            }

            foreach (var fieldValue in response.Enums)
            {
                yield return new Level
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
    }
}