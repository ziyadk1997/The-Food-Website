// *********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
// *********************************************************

namespace AuthManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ManagementResource
    {
        public static string ApiBaseURL
        {
            get
            {
                return "https://management.azure.com/";
            }
        }
        
        public static string GetAppSettingsUrl
        {
            get
            {
                return AppSettingsUrl + "/list?" + ApiVersion;
            }
        }

        public static string UpdateAppSettingsUrl
        {
            get
            {
                return AppSettingsUrl + "?" + ApiVersion;
            }
        }

        private static string AppSettingsUrl
        {
            get
            {
                return ApiBaseURL + "subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Web/sites/{2}/config/appsettings";
            }
        }

        private static string ApiVersion
        {
            get
            {
                return "api-version=2015-08-01";
            }
        }

        public static string GetRequestUrl(string resourceRequested, Guid subscriptionId, string resourceGroupName,  string applicationName)
        {
            return string.Format(resourceRequested, subscriptionId.ToString(), resourceGroupName, applicationName);
        }
    }
}
