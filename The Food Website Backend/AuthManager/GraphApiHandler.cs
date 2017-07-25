// *********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
// *********************************************************

namespace AuthManager
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GraphApiHandler
    {
        private AuthLib authClient;

        public GraphApiHandler(Guid clientID, string clientSecret)
        {
            this.authClient = new AuthLib(clientID, clientSecret);
        }

        /// <summary>
        /// Use this function to get all the users inside an Azure Active directory.
        /// this Method is using application Credentials. 
        /// the application entry inside the AD should have been granted Directory.Read.All permission scope for '
        /// the "windows Azure Active directory" API to execute this function.
        /// doesn't require a signed in user.
        /// </summary>
        /// <Note>the Directory.Read.All permission scope is displayed "Read directory data" in the "application permissions" 
        /// which can be found in the "required permissions" tab in you AD application entry.</Note>
        /// <returns>A list of user objects.</returns>
        public async Task<List<User>> GetAllUsers()
        {
            return await this.GetAllUsers(GraphResource.Users);
        }

        public async Task<User> GetUser(Guid userId)
        {
            return (await this.GetAllUsers(string.Format(GraphResource.User, userId))).FirstOrDefault();
        }

        public async Task<bool> IsRegistered(Guid userId)
        {
            return (await this.GetAllUsers(string.Format(GraphResource.User, userId))).FirstOrDefault() != null;
        }

        public async Task<bool> IsRegistered(string userEmail)
        {
            string filterQuery = "(mail eq '" + userEmail + "')" +
                " or (userPrincipalName eq '" + userEmail + "')";

            return (await this.GetAllUsers(string.Format(GraphResource.FilteredUsers, filterQuery))).FirstOrDefault() != null;
        }

        public async Task<List<User>> SearchUsers(string searchText)
        {
            var queryString = "startswith(displayName,'" + searchText +
                  "') or startswith(givenName,'" + searchText +
                  "') or startswith(surname,'" + searchText +
                  "') or startswith(userPrincipalName,'" + searchText +
                  "') or startswith(mail,'" + searchText +
                  "')";

            return await this.GetAllUsers(string.Format(GraphResource.FilteredUsers, queryString));
        }

        private async Task<List<User>> GetAllUsers(string graphRequestResource)
        {
            return this.GetUserInfoFromGraphResponse(await this.SendGraphGetRequest(graphRequestResource));
        }

        private async Task<string> SendRequest(HttpRequestMessage graphRequest)
        {
            var httpClient = await this.HttpClientObject();

            HttpResponseMessage httpRequestResult;

            try
            {
                httpRequestResult = await httpClient.SendAsync(graphRequest);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Failed to get user send this Get Request: " + graphRequest.RequestUri +
                    "the request Authorization IsNullOrWhiteSpace: " + string.IsNullOrWhiteSpace(graphRequest.Headers.Authorization.ToString()) + ".\nexception: " + ex.ToString());
                throw;
            }

            return await httpRequestResult.Content.ReadAsStringAsync();
        }

        private List<User> GetUserInfoFromGraphResponse(string response)
        {
            try
            {
                dynamic users = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(response);
                var usersList = new List<User>();

                // More than 1 User
                if (users.value != null)
                {
                    foreach (var user in users.value)
                    {
                        if (user.objectType != null && (user.objectType == "User"))
                        {
                            usersList.Add(this.GetUserData(user));
                        }
                    }
                }
                else
                {
                    if (users.objectType != null && (users.objectType == "User"))
                    {
                        usersList.Add(this.GetUserData(users));
                    }
                    else
                    {
                        return null;
                    }
                }

                return usersList;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Failed to extract user information From this Graph Response: " + response + ".\nexception: " + ex.ToString());
                throw;
            }
        }

        private async Task<HttpClient> HttpClientObject()
        {
            var httpClient = new HttpClient();

            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await this.authClient.AccessToken(GraphResource.ApiBaseURL));
            }
            catch (Exception ex)
            {
                Trace.TraceError("Failed to get an access token for " + GraphResource.ApiBaseURL + ".\nexception: " + ex.ToString());
                throw;
            }

            return httpClient;
        }

        private async Task<string> SendGraphGetRequest(string graphRequestResource)
        {
            var graphRequest = new HttpRequestMessage(HttpMethod.Get, GraphResource.GetRequestURI(AuthLib.UserTenantId.ToString(), graphRequestResource));

            return await this.SendRequest(graphRequest);
        }

        private User GetUserData(dynamic user)
        {
            try
            {
                return new User(
                        Guid.Parse(user.objectId.ToString()),
                        user.displayName.ToString(),
                        (user.mail == null) ? (user.otherMails.Count > 0 ? user.otherMails[0].ToString() : string.Empty) : user.mail.ToString(),
                        user.givenName.ToString(),
                        user.surname.ToString());
            }
            catch (Exception ex)
            {
                Trace.TraceError("Failed to get user information.\nexception: " + ex.ToString());
                throw;
            }
        }
    }
}
