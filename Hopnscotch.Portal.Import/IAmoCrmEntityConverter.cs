using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Import
{
    public interface IAmoCrmEntityConverter
    {
        Contact Convert(ApiIndividualContactResponse response);
        Lead Convert(ApiLeadResponse response);
        Task Convert(ApiTaskResponse response);
    }
}