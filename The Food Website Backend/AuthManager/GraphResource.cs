// *********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
// *********************************************************

namespace AuthManager
{
    public static class GraphResource
    {
        public static string Users
        {
            get
            {
                return UsersResourceBaseString + "?" + ApiVersion;
            }
        }

        public static string Applications
        {
            get
            {
                return ApplicationsBaseString + "?" + ApiVersion;
            }
        }

        public static string FilteredUsers
        {
            get
            {
                return Users + "&$filter={0}";
            }
        }

        public static string FilteredApplications
        {
            get
            {
                return Applications + "&$filter={0}";
            }
        }

        public static string User
        {
            get
            {
                return UsersResourceBaseString + "/{0}?" + ApiVersion;
            }
        }

        public static string Application
        {
            get
            {
                return ApplicationsBaseString + "/{0}?" + ApiVersion;
            }
        }

        public static string Groups
        {
            get
            {
                return GroupsResourceBaseString + "?" + ApiVersion;
            }
        }

        public static string GroupMembers
        {
            get
            {
                return GroupsResourceBaseString + "/{0}/members?" + ApiVersion;
            }
        }

        public static string GroupMembersLink
        {
            get
            {
                return GroupMembersLinkBase + "?" + ApiVersion;
            }
        }

        public static string GroupMemberLink
        {
            get
            {
                return GroupMembersLinkBase + "/{0}?" + ApiVersion;
            }
        }

        public static string ApiBaseURL
        {
            get
            {
                return "https://graph.windows.net/";
            }
        }

        /// <summary>
        /// Gets the requestUrl in the following format
        /// GraphBaseURL + Tenant ID + Requested Resource
        /// </summary>
        private static string RequestURL
        {
            get
            {
                return ApiBaseURL + "{0}{1}";
            }
        }

        private static string GroupMembersLinkBase
        {
            get
            {
                return GroupsResourceBaseString + "/{0}/$links/members";
            }
        }

        private static string GroupsResourceBaseString
        {
            get
            {
                return "/groups";
            }
        }

        private static string UsersResourceBaseString
        {
            get
            {
                return "/users";
            }
        }

        private static string ApplicationsBaseString
        {
            get
            {
                return "/applications";
            }
        }

        private static string ApiVersion
        {
            get
            {
                return "api-version=1.6";
            }
        }

        public static System.Uri GetRequestURI(string tenantId, string resource)
        {
            return new System.Uri(string.Format(GraphResource.RequestURL, tenantId, resource));
        }
    }
}