using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Owin.Security;
using Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using AuthManager;

namespace FoodWebsite
{
    public partial class Startup
    {
        public async Task<bool> Auth(System.Security.Claims.ClaimsIdentity userIdentity)
        {
            return await Task.FromResult<bool>(true);
        }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            AuthLib.AuthStartupInitialize(app, "ba29dba3-f782-4767-ba4b-b24ee47b35b6", "vCuDmHbepxHBEKnvzEFofDgRaheuhdXKRMYU937MBvE=", "Shared/Error?errorMessage={0}", this.Auth, appTenantId: "72f988bf-86f1-41af-91ab-2d7cd011db47");

        }
    }
}
