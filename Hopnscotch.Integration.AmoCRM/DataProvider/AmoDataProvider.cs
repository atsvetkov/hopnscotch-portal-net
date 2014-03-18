﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Hopnscotch.Integration.AmoCRM.DataProvider;
using Hopnscotch.Integration.AmoCRM.Entities;

namespace Hopnscotch.Integration.AmoCRM
{
    public sealed class AmoDataProvider : IAmoDataProvider
    {
        private const string ApiBaseUrlTemplate = "https://{0}.amocrm.ru/";
        private const string ApiAuthorizationUrlTail = "private/api/auth.php?type=json";
        private const string ApiGetContactsUrlTail = "private/api/v2/json/contacts/list";
        private const string ApiGetLeadsUrlTail = "private/api/v2/json/leads/list";
        private const string ApiGetTasksUrlTail = "private/api/v2/json/tasks/list";
        private const string ApiGetContactLeadLinksUrlTail = "private/api/v2/json/contacts/links";
        
        private readonly string _subDomain;
        private readonly string _login;
        private readonly string _hash;

        private readonly HttpClientHandler _handler;
        private readonly HttpClient _client;

        public AmoDataProvider(string subDomain, string login, string hash)
        {
            _subDomain = subDomain;
            _login = login;
            _hash = hash;

            _handler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = new CookieContainer()
            };
            
            _client = new HttpClient(_handler);

            _client.BaseAddress = new Uri(ApiBaseUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private string ApiBaseUrl
        {
            get { return string.Format(ApiBaseUrlTemplate, _subDomain); }
        }

        public async Task<bool> AuthenticateAsync()
        {
            var authParams = new ApiAuthParameters(_login, _hash);
            var response = await _client.PostAsJsonAsync(new Uri(ApiAuthorizationUrlTail, UriKind.Relative), authParams);
            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<ApiResponseRoot<ApiAuthResponse>>().Result.Response.IsAuthorized;
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

        private async Task<ApiResponseRoot<T>> GetEntitiesAsync<T>(string relativeUrl) where T : class
        {
            var response = await _client.GetAsync(new Uri(relativeUrl, UriKind.Relative));

            return await response.Content.ReadAsAsync<ApiResponseRoot<T>>();
        }
    }
}
