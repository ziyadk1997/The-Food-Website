using AuthManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.UserIdentity
{
    public static class UserIdentityManager
    {
        private static Dictionary<string, Guid> usersId = new Dictionary<string, Guid>();

        private static Dictionary<Guid, string> usersEmail = new Dictionary<Guid, string>();

        public static Guid GetUserId()
        {
            string userEmail = GetUserEmail();
            if(usersId.ContainsKey(userEmail))
            {
                return usersId[userEmail];
            }
            else
            {
                Guid userId = Guid.NewGuid();
                usersId.Add(userEmail, userId);
                usersEmail.Add(userId, userEmail);
                return userId;
            }
        }

        public static string GetUserEmail()
        {
            string email = AuthLib.UserEmail;
            if(email != null)
            {
                return email;
            }
            else
            {
                throw new Exception("Cannot fetch user");
            }
        }

        public static String GetUserEmail(Guid id)
        {
            return usersEmail[id];
        }
        public static Guid GetUserID(String email)
        {
            return usersId[email];
        }

    }
}
