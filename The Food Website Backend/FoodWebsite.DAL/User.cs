using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodWebsite.DAL
{
    public class User
    {
        private static Dictionary<Guid,User> Users { set; get; } = new Dictionary<Guid, User>();
        public Guid UserID { set; get; }
        public String Name { set; get; }
        public String Email { set; get; }

        public static void Add(Guid id, User user)
        {
            Users.Add(id, user);
        }
        

        public static User Get(Guid id)
        {
            return Users[id];
        }
    }
}
