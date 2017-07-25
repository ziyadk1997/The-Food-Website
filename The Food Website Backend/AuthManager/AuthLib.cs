// *********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
// *********************************************************

namespace AuthManager
{
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using Microsoft.Owin.Security.OpenIdConnect;
    using Owin;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;

    public enum AuthPermissionsScope
    {
        DelegatedUserPermissions,
        ApplicationOnlyScope
    }

    public class AuthLib
    {
        public static readonly string TenantIdClaimKey = "http://schemas.microsoft.com/identity/claims/tenantid";

        private static readonly string ObjectIdentifierClaimKey = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        private static readonly string UserIdTokenClaimKey = "user_Id_Token";
        private static readonly string UserAccessTokenClaimKeyPrefix = "user_Access_Token_for_";
        private static readonly string UserDisplayNameClaimKey = "name";
        private static readonly string AzureActiveDirectoryBaseAddress = "https://login.microsoftonline.com/{0}";

        private AuthPermissionsScope currentPermissionScope;

        public AuthLib(Guid clientId, string clientSecret) : this(clientId, clientSecret, AuthPermissionsScope.DelegatedUserPermissions)
        {
            // TODO: check in the case of multi-tenant delegated permissions scope whitch tenant id is sent (the user, the application)
            this.TenantId = UserTenantId;
        }

        public AuthLib(Guid clientId, string clientSecret, Guid tenantId) : this(clientId, clientSecret, AuthPermissionsScope.ApplicationOnlyScope)
        {
            this.TenantId = tenantId;
        }

        private AuthLib(Guid clientId, string clientSecret, AuthPermissionsScope currentScope)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.currentPermissionScope = currentScope;
        }

        public static string UserIdToken
        {
            get
            {
                return ClaimsPrincipal.Current.FindFirst(UserIdTokenClaimKey).Value;
            }
        }

        public static Guid UserTenantId
        {
            get
            {
                return Guid.Parse(ClaimsPrincipal.Current.FindFirst(TenantIdClaimKey).Value);
            }
        }

        public static string UserFirstName
        {
            get
            {
                return ClaimsPrincipal.Current.FindFirst(ClaimTypes.GivenName).Value;
            }
        }

        public static string UserLastName
        {
            get
            {
                return ClaimsPrincipal.Current.FindFirst(ClaimTypes.Surname).Value;
            }
        }

        public static string UserEmail
        {
            get
            {
                if (ClaimsPrincipal.Current == null)
                {
                    return null;
                }

                try
                {   
                    return ClaimsPrincipal.Current.FindFirst(ClaimTypes.Upn).Value;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static string UserDisplayName
        {
            get
            {
                return ClaimsPrincipal.Current.FindFirst(UserDisplayNameClaimKey).Value;
            }
        }

        public static Guid UserObjectId
        {
            get
            {
                return Guid.Parse(ClaimsPrincipal.Current.FindFirst(ObjectIdentifierClaimKey).Value);
            }
        }

        public AuthPermissionsScope CurrentPermissionScope
        {
            get { return this.currentPermissionScope; }
        }

        private Guid TenantId { get; set; }

        private Guid ClientId { get; set; }

        private string ClientSecret { get; set; }

        public static Guid GetUserObjectId(ClaimsIdentity identity)
        {
            return Guid.Parse(identity.Claims.FirstOrDefault(c => c.Type == ObjectIdentifierClaimKey).Value);
        }

        public static HttpClient GetInternalHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", UserIdToken);
            return client;
        }

        public static void AuthStartupInitialize(IAppBuilder app, string appId, string appKey, string authorizationFailRedirectUri, Func<ClaimsIdentity, Task<bool>> authorizedUser, string redirectUri = "", string appTenantId = "common", bool usingGraphApiAsUser = false)
        {
            // Configure OpenIDConnect, register callbacks for OpenIDConnect Notifications
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = appId,
                ClientSecret = appKey,
                Authority = string.Format(CultureInfo.InvariantCulture, AzureActiveDirectoryBaseAddress, appTenantId),
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                AuthenticationType = AuthenticationTypes.Federation,
                TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = (appTenantId == "common") ? false : true,
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                },

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    RedirectToIdentityProvider = (context) =>
                    {
                        Trace.TraceInformation("AuthManager.RedirectToIdentityProvider {redirectUri: " + redirectUri +
                            ",  HttpContext.Current.Request.Url: " + HttpContext.Current.Request.Url +
                            ",  string.IsNullOrWhiteSpace(redirectUri): " + string.IsNullOrWhiteSpace(redirectUri) +
                            ",  UriPartial.Authority: " + UriPartial.Authority +
                            ",  HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority): " + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " }");

                        if (string.IsNullOrWhiteSpace(redirectUri))
                        {
                            redirectUri = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                            context.ProtocolMessage.RedirectUri = redirectUri;
                            context.ProtocolMessage.PostLogoutRedirectUri = redirectUri;
                        }
                        else
                        {
                            context.ProtocolMessage.RedirectUri = redirectUri;
                            context.ProtocolMessage.PostLogoutRedirectUri = redirectUri;
                        }

                        return Task.FromResult(0);
                    },

                    SecurityTokenValidated = async (context) =>
                    {
                        if (await authorizedUser(context.AuthenticationTicket.Identity))
                        {
                            return;
                        }
                        else
                        {
                            throw new UnauthorizedAccessException("UnAuthorized.");
                        }
                    },

                    AuthorizationCodeReceived = async (context) =>
                    {
                        context.AuthenticationTicket.Identity.AddClaim(new Claim(UserIdTokenClaimKey, context.ProtocolMessage.IdToken));
                        if (!string.IsNullOrWhiteSpace(context.Code))
                        {
                            Guid userTenantId = Guid.Parse(context.AuthenticationTicket.Identity.Claims.FirstOrDefault(c => c.Type == TenantIdClaimKey).Value);
                            if (usingGraphApiAsUser)
                            {
                                context.AuthenticationTicket.Identity.AddClaim(
                                    new Claim(UserAccessTokenClaimKeyPrefix + GraphResource.ApiBaseURL, await GetUserAccessToken(Guid.Parse(appId), appKey, userTenantId, context.Code, GraphResource.ApiBaseURL)));
                            }
                        }
                        else
                        {
                            Trace.TraceError("Authorization Code Received is Is Null Or White Space (" + context.Code + ")\nThe Parameters:{" +
                                "appId: " + appId + ", " +
                                "authorizationFailRedirectUri: " + authorizationFailRedirectUri + ", " +
                                "redirectUri: " + redirectUri + ", " +
                                "appTenantId: " + appTenantId +
                                "}");
                        }

                        return;
                    },

                    AuthenticationFailed = (context) =>
                    {
                        context.HandleResponse();
                        context.Response.Redirect(string.Format(authorizationFailRedirectUri, context.Exception.Message));
                        return Task.FromResult(0);
                    }
                }
            });
        }

        public async Task<string> AccessToken(string resourceBaseURL)
        {
            string authority = string.Format(CultureInfo.InvariantCulture, AzureActiveDirectoryBaseAddress, this.TenantId);

            ClientCredential clientCred = new ClientCredential(this.ClientId.ToString(), this.ClientSecret);
            AuthenticationContext authContext;
            AuthenticationResult assertionCredential;

            if (this.currentPermissionScope == AuthPermissionsScope.DelegatedUserPermissions)
            {
                return ClaimsPrincipal.Current.FindFirst(UserAccessTokenClaimKeyPrefix + resourceBaseURL).Value;
            }
            else
            {
                authContext = new AuthenticationContext(authority);

                assertionCredential = await authContext.AcquireTokenAsync(resourceBaseURL, clientCred);
                return assertionCredential.AccessToken;
            }
        }

        private static async Task<string> GetUserAccessToken(Guid clientId, string clientSecret, Guid tenantId, string theCode, string resource)
        {
            string authority = string.Format(CultureInfo.InvariantCulture, AzureActiveDirectoryBaseAddress, tenantId);
            ClientCredential clientCred;

            clientCred = new ClientCredential(clientId.ToString(), clientSecret);

            AuthenticationContext authContext;
            AuthenticationResult assertionCredential;

            authContext = new AuthenticationContext(authority);
            if (string.IsNullOrWhiteSpace(theCode))
            {
                Trace.TraceError("Failed to optain an access token for resource: " + resource + "\nThe Parameters:{" +
                              "clientId: " + clientId.ToString() + ", " +
                              "tenantId: " + tenantId.ToString() + ", " +
                              "theCode is " + (string.IsNullOrWhiteSpace(theCode) ? "equal To" : "not") + "null, " +
                              "redirectUri: " + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "}");
            }

            try
            {
                Trace.TraceInformation("while getting the access token the code is " + (string.IsNullOrWhiteSpace(theCode) ? "equal To" : "not") + "null");
                assertionCredential = await authContext.AcquireTokenByAuthorizationCodeAsync(
                                theCode, new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)), clientCred, resource);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Failed to optain an access token for resource: " + resource + "\nThe Parameters:{" +
                               "clientId: " + clientId.ToString() + ", " +
                               "tenantId: " + tenantId.ToString() + ", " +
                               "theCode is " + (string.IsNullOrWhiteSpace(theCode) ? "equal To" : "not") + "null, " +
                               "redirectUri: " + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +
                               "}\n Exception: " + ex);
                throw;
            }

            return assertionCredential.AccessToken;
        }
    }
}