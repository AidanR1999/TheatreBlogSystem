using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreBlogSystem.Models;
using TheatreBlogSystem.ViewModels;

namespace TheatreBlogSystem.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// loads the home page
        /// </summary>
        /// <returns>Home Page</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}