using System.Threading.Tasks;
using Hopnscotch.Integration.AmoCRM.Entities;

namespace Hopnscotch.Integration.AmoCRM.DataProvider
{
    public interface IAmoDataProvider
    {
        Task<bool> AuthenticateAsync();

        Task<ApiResponseRoot<ApiContactListResponse>> GetContactsAsync();
        
        Task<ApiResponseRoot<ApiLeadListResponse>> GetLeadsAsync();

        Task<ApiResponseRoot<ApiTaskListResponse>> GetTasksAsync();

        Task<ApiResponseRoot<ApiContactLeadLinkListResponse>> GetContactLeadLinksAsync();
    }
}