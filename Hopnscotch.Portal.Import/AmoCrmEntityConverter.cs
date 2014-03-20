using System;
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
                Created = GetDateTimeOrDefault(response.Created),
                Modified = GetDateTimeOrDefault(response.LastModified),
                Name = response.Name
            };
        }

        public Lead Convert(ApiLeadResponse response)
        {
            return new Lead
            {
                AmoId = response.Id,
                Name = response.Name,
                Created = GetDateTimeOrDefault(response.Created),
                Modified = GetDateTimeOrDefault(response.LastModified),
                Price = response.Price
            };
        }

        public Task Convert(ApiTaskResponse response)
        {
            throw new NotImplementedException();
        }

        private DateTime? GetDateTimeOrDefault(DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ? default(DateTime?) : dateTime;
        }
    }
}