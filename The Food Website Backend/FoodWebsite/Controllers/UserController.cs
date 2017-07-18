using FoodWebsite.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodWebsite.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public void Add(String name, String email)
        {
            User user = new User
            {
                Name = name,
                Email = email,
                UserID= Guid.NewGuid(),
                
            };
            DAL.User.Add(user.UserID,user);
        }
    }
}
