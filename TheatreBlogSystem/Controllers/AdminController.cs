using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreBlogSystem.Models;

namespace TheatreBlogSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        //GET: Approve Post
        public ActionResult ApprovePosts()
        {
            ApplicationDbContext db = ApplicationDbContext.Create();

            var model = db.Posts;
            return View(model);
        }
    }
}