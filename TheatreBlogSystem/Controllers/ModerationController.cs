using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreBlogSystem.Models;

namespace TheatreBlogSystem.Controllers
{
    /// <summary>
    /// The main handler for actions that relate to the system management
    /// </summary>
    public class ModerationController : Controller
    {
        // GET: Admin
        /// <summary>
        /// loads the system management page
        /// </summary>
        /// <returns>System Management Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        public ActionResult Index()
        {
            return View();
        }
    }
}