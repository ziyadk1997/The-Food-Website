// *********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
// *********************************************************

namespace AuthManager
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class RequireHttps : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            Trace.TraceInformation("AuthManager.RequireHttps {filterContext.HttpContext.Request.IsLocal: "
                + filterContext.HttpContext.Request.IsLocal + ", filterContext.HttpContext.Request: " + filterContext.HttpContext.Request + " }");

            if (filterContext.HttpContext != null && filterContext.HttpContext.Request.IsLocal)
            {
                return;
            }

            base.OnAuthorization(filterContext);
        }
    }
}
