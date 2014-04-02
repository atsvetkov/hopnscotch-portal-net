using System.Threading.Tasks;
using Hopnscotch.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;

namespace Hopnscotch.Portal.Integration.AmoCRM.DataProvider
{
    public interface IAmoDataProvider
    {
        Task<bool> AuthenticateAsync();

        Task<ApiResponseRoot<ApiAccountRootResponse>> GetAccountAsync();

        Task<ApiResponseRoot<ApiContactListResponse>> GetContactsAsync();
        
        Task<ApiResponseRoot<ApiLeadListResponse>> GetLeadsAsync();

        Task<ApiResponseRoot<ApiTaskListResponse>> GetTasksAsync();

        Task<ApiResponseRoot<ApiContactLeadLinkListResponse>> GetContactLeadLinksAsync();

        bool Authenticate();

        ApiResponseRoot<ApiAccountRootResponse> GetAccount();

        ApiResponseRoot<ApiContactListResponse> GetContacts();

        ApiResponseRoot<ApiLeadListResponse> GetLeads();

        ApiResponseRoot<ApiTaskListResponse> GetTasks();

        ApiResponseRoot<ApiContactLeadLinkListResponse> GetContactLeadLinks();
    }
}