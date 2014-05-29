using System.Collections.Generic;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Import
{
    public interface IAmoCrmEntityConverter
    {
        Contact Convert(ApiIndividualContactResponse response);
        Lead Convert(ApiLeadResponse response);
        Task Convert(ApiTaskResponse response);
        User Convert(ApiUserResponse response);
        LeadStatus Convert(ApiLeadStatusResponse response);
        IEnumerable<T> Convert<T>(ApiCustomFieldDefinitionResponse response) where T : AmoNamedEntityBase, new();
    }
}