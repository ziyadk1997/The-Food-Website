// *********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
// *********************************************************

namespace AuthManager
{
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    public class ManagementApiHandler
    {
        private Guid subscriptionId;
        private AuthLib authManager;

        public ManagementApiHandler(Guid clientId, string clientSecret, Guid subscriptionId, Guid tenantId)
        {
            this.subscriptionId = subscriptionId;
            this.authManager = new AuthLib(clientId, clientSecret, tenantId);
        }

        public async Task<JObject> GetAppSettings(string resourceGroupName, string applicationName)
        {
            return await this.SendPostRequest(ManagementResource.GetRequestUrl(ManagementResource.GetAppSettingsUrl, this.subscriptionId, resourceGroupName, applicationName));
        }

        public async Task<string> UpdateAppSettings(string resourceGroupName, string applicationName, object newSettings)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

            var result = await this.SendPutRequest(
                ManagementResource.GetRequestUrl(ManagementResource.UpdateAppSettingsUrl, this.subscriptionId, resourceGroupName, applicationName),
                newSettings);

            var response = JObject.Parse(jsonSerializer.Serialize(result));
            if (response.Count == 1)
            {
                var error = new
                {
                    status = 1,
                    errorCode = response.SelectToken("error.code"),
                    errorMessage = response.SelectToken("error.message")
                };

                throw new Exception(new JavaScriptSerializer().Serialize(error));
            }

            return jsonSerializer.Serialize(result);
        }
        
        private async Task<JObject> SendPostRequest(string requestUrl)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, new Uri(requestUrl));

            var httpClient = await this.GetHttpClient();

            var httpRequestResult = await httpClient.SendAsync(request);

            var result = await httpRequestResult.Content.ReadAsStringAsync();

            return JObject.Parse(result);
        }

        private async Task<object> SendPutRequest(string requestUrl, object requestContent)
        {
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

            var httpClient = await this.GetHttpClient();

            HttpRequestMessage httpPutRequest = new HttpRequestMessage(HttpMethod.Put, new Uri(requestUrl))
            {
                Content = new StringContent(requestContent.ToString(), Encoding.UTF8, "application/json")
            };

            var httpRequestResult = await httpClient.SendAsync(httpPutRequest);

            var httpRequestContentString = await httpRequestResult.Content.ReadAsStringAsync();

            return jsonSerializer.DeserializeObject(httpRequestContentString);
        }

        private async Task<HttpClient> GetHttpClient()
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await this.authManager.AccessToken(ManagementResource.ApiBaseURL));

            return httpClient;
        }
    }
}
