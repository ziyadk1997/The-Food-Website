using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodWebsite.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
    }
}