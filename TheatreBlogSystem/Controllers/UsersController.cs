using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TheatreBlogSystem.Models;
using WebGrease;

namespace TheatreBlogSystem.Controllers
{
    public class UsersController : AccountController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersController() : base()
        {

        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base()
        {

        }

        // GET: Users
        /// <summary>
        /// load the users index page, only admins and mods can access
        /// </summary>
        /// <returns>User Index Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        /// <summary>
        /// show details of the selected user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User Details Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        /// <summary>
        /// load the create user page
        /// </summary>
        /// <returns>Create User Page</returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        /// <summary>
        /// get the details entered from the create user page and add the new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="userRole"></param>
        /// <returns>User Index Page</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,TimeOfRegistration,Forename,Surname,DateOfBirth,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] Staff user, string password, string userRole)
        {
            user.TimeOfRegistration = DateTime.Now;
            var newUser = new Staff { UserName = user.Email, Email = user.Email, TimeOfRegistration = DateTime.Now, DateOfBirth = user.DateOfBirth, Forename = user.Forename, Surname = user.Surname };
            var result = await UserManager.CreateAsync(newUser, password);

            var userId = UserManager.FindByEmail(newUser.Email).Id;

            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(userId, userRole);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Delete/5
        /// <summary>
        /// loads the delete user page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete User Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        /// <summary>
        /// delete the selected user from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User Index Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// allows the admin to promote, suspend and unsuspend users
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newRole"></param>
        /// <param name="postId"></param>
        /// <returns>User Index Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult> UpdateRole(string id, string newRole, int? postId)
        {
            if (id == User.Identity.GetUserId() || newRole == null)
                return RedirectToAction("Index", "Users");

            ApplicationDbContext db = ApplicationDbContext.Create();

            User user = db.Users.Find(id);
            string oldRole = (user.CurrentRole);

            if (oldRole != "Customer" && newRole == "Suspended")
                return RedirectToAction("Index", "Users");

            await UserManager.RemoveFromRoleAsync(id, oldRole);
            await UserManager.AddToRoleAsync(id, newRole);

            if (postId == null)
                return RedirectToAction("Index", "Users");

            return RedirectToAction("PostDetails", "Posts", new { postId = postId });
        }
    }
}
