using System.Threading.Tasks;

namespace Hopnscotch.Integration.AmoCRM
{
    public interface IAmoDataProvider
    {
        Task<bool> AuthenticateAsync();

        //TODO: in future this response class should be transformed into domain entity
        Task<ApiResponseRoot<ApiContactListResponse>> GetContactsAsync();
    }
}