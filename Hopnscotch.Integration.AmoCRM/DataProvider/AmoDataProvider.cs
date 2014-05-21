using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
    public sealed class AmoDataProvider : IAmoDataProvider
    {
        private const string ApiBaseUrlTemplate = "https://{0}.amocrm.ru/";
        private const string ApiAuthorizationUrlTail = "private/api/auth.php?type=json";
        private const string ApiGetContactsUrlTail = "private/api/v2/json/contacts/list";
        private const string ApiGetLeadsUrlTail = "private/api/v2/json/leads/list";
        private const string ApiGetTasksUrlTail = "private/api/v2/json/tasks/list";
        private const string ApiGetContactLeadLinksUrlTail = "private/api/v2/json/contacts/links";
        private const string ApiGetAccountUrlTail = "private/api/v2/json/accounts/current";
        
        private readonly string subDomain;
        private readonly string login;
        private readonly string hash;

        private readonly HttpClientHandler handler;
        private readonly HttpClient client;

        private readonly IAttendanceUow attendanceUow;

        public AmoDataProvider(IConfig config, IAttendanceUow attendanceUow)
        {
            this.attendanceUow = attendanceUow;
            subDomain = config.AmoSubDomain;
            login = config.AmoLogin;
            hash = config.AmoHash;

            handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            
            client = new HttpClient(handler);

            client.BaseAddress = new Uri(ApiBaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private string ApiBaseUrl
        {
            get { return string.Format(ApiBaseUrlTemplate, subDomain); }
        }

        public async Task<bool> AuthenticateAsync()
        {
            var authParams = new ApiAuthParameters(login, hash);
            var response = await client.PostAsJsonAsync(new Uri(ApiAuthorizationUrlTail, UriKind.Relative), authParams);
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<ApiResponseRoot<ApiAuthResponse>>().Result.Response.IsAuthorized;
        }

        public bool Authenticate()
        {
            var authParams = new ApiAuthParameters(login, hash);
            var response = client.PostAsJsonAsync(new Uri(ApiAuthorizationUrlTail, UriKind.Relative), authParams).Result;
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<ApiResponseRoot<ApiAuthResponse>>().Result.Response.IsAuthorized;
        }

        public async Task<ApiResponseRoot<ApiAccountRootResponse>> GetAccountAsync()
        {
            return await GetEntitiesAsync<ApiAccountRootResponse>(ApiGetAccountUrlTail);
        }

        public async Task<ApiResponseRoot<ApiContactListResponse>> GetContactsAsync()
        {
            return await GetEntitiesAsync<ApiContactListResponse>(ApiGetContactsUrlTail);
        }

        public async Task<ApiResponseRoot<ApiLeadListResponse>> GetLeadsAsync()
        {
            return await GetEntitiesAsync<ApiLeadListResponse>(ApiGetLeadsUrlTail);
        }

        public async Task<ApiResponseRoot<ApiTaskListResponse>> GetTasksAsync()
        {
            return await GetEntitiesAsync<ApiTaskListResponse>(ApiGetTasksUrlTail);
        }

        public async Task<ApiResponseRoot<ApiContactLeadLinkListResponse>> GetContactLeadLinksAsync()
        {
            return await GetEntitiesAsync<ApiContactLeadLinkListResponse>(ApiGetContactLeadLinksUrlTail);
        }
        

        public ApiResponseRoot<ApiAccountRootResponse> GetAccount()
        {
            return GetEntities<ApiAccountRootResponse>(ApiGetAccountUrlTail);
        }

        public ApiResponseRoot<ApiContactListResponse> GetContacts()
        {
            return GetEntities<ApiContactListResponse>(ApiGetContactsUrlTail);
        }

        public ApiResponseRoot<ApiLeadListResponse> GetLeads()
        {
            return GetEntities<ApiLeadListResponse>(ApiGetLeadsUrlTail);
        }

        public ApiResponseRoot<ApiTaskListResponse> GetTasks()
        {
            return GetEntities<ApiTaskListResponse>(ApiGetTasksUrlTail);
        }

        public ApiResponseRoot<ApiContactLeadLinkListResponse> GetContactLeadLinks()
        {
            return GetEntities<ApiContactLeadLinkListResponse>(ApiGetContactLeadLinksUrlTail);
        }

        public bool SaveDataDuringImport { get; set; }
        
        private async Task<ApiResponseRoot<T>> GetEntitiesAsync<T>(string relativeUrl) where T : class
        {
            var response = await client.GetAsync(new Uri(relativeUrl, UriKind.Relative));

            return await response.Content.ReadAsAsync<ApiResponseRoot<T>>();
        }

        private ApiResponseRoot<T> GetEntities<T>(string relativeUrl) where T : class
        {
            var response = client.GetAsync(new Uri(relativeUrl, UriKind.Relative)).Result;
            if (SaveDataDuringImport)
            {
                var result = response.Content.ReadAsByteArrayAsync().Result;
                var responseString = Encoding.UTF8.GetString(result, 0, result.Length - 1);
                
                //var responseString = JsonConvert.SerializeObject(response);
                SaveImportData<T>(responseString);
            }

            return response.Content.ReadAsAsync<ApiResponseRoot<T>>().Result;
        }

        private void SaveImportData<T>(string responseString)
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
                importData = new ImportData
                {
                    ResponseType = attribute.ResponseType,
                    ResponseData = responseString
                };

                attendanceUow.ImportData.Add(importData);
            }
            else
            {
                importData.ResponseData = responseString;
                attendanceUow.ImportData.Update(importData);
            }

            attendanceUow.Commit();
        }
    }
}
