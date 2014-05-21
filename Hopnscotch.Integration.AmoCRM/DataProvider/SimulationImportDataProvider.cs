using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hopnscotch.Integration.AmoCRM;
using Hopnscotch.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Integration.AmoCRM.Extensions;
using Hopnscotch.Portal.Model;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.DataProvider
{
    public sealed class SimulationImportDataProvider : IAmoDataProvider
    {
        private readonly IAttendanceUow attendanceUow;

        public SimulationImportDataProvider(IAttendanceUow attendanceUow)
        {
            this.attendanceUow = attendanceUow;
        }

        #region Not used

        public Task<bool> AuthenticateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseRoot<ApiAccountRootResponse>> GetAccountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseRoot<ApiContactListResponse>> GetContactsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseRoot<ApiLeadListResponse>> GetLeadsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseRoot<ApiTaskListResponse>> GetTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponseRoot<ApiContactLeadLinkListResponse>> GetContactLeadLinksAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

        public bool Authenticate()
        {
            return true;
        }

        private ApiResponseRoot<T> FindSavedImportData<T>() where T : class
        {
            var type = typeof(T);
            var attribute = type.GetAttribute<AmoCrmResponseTypeAttribute>();
            if (attribute == null)
            {
                throw new AttributeMissingException("Could not save import data: type " + type.Name + " is not marked with " + typeof(AmoCrmResponseTypeAttribute).Name + " attribute.");
            }

            var importData = attendanceUow.ImportData.GetByResponseType(attribute.ResponseType);
            if (importData == null)
            {
                throw new ImportSimulationException("No saved import data for '" + attribute.ResponseType + "', import simulation failed!");
            }

            // TODO: Fix deserialization issues
            return JsonConvert.DeserializeObject<ApiResponseRoot<T>>(importData.ResponseData);
        }

        public ApiResponseRoot<ApiAccountRootResponse> GetAccount()
        {
            return FindSavedImportData<ApiAccountRootResponse>();
        }

        public ApiResponseRoot<ApiContactListResponse> GetContacts()
        {
            return FindSavedImportData<ApiContactListResponse>();
        }

        public ApiResponseRoot<ApiLeadListResponse> GetLeads()
        {
            return FindSavedImportData<ApiLeadListResponse>();
        }

        public ApiResponseRoot<ApiTaskListResponse> GetTasks()
        {
            return FindSavedImportData<ApiTaskListResponse>();
        }

        public ApiResponseRoot<ApiContactLeadLinkListResponse> GetContactLeadLinks()
        {
            return FindSavedImportData<ApiContactLeadLinkListResponse>();
        }

        public bool SaveDataDuringImport { get; set; }
    }
}