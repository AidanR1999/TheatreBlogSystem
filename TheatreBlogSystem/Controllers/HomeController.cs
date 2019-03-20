using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreBlogSystem.Models;
using TheatreBlogSystem.ViewModels;

namespace TheatreBlogSystem.Controllers
{
    /// <summary>
    /// The main handler for actions that relate to the home page
    /// </summary>
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

        /// <summary>
        /// loads the contact page
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            return View();
        }
    }
}