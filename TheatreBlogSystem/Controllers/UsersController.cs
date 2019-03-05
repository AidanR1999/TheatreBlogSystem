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
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TimeOfRegistration,Forename,Surname,DateOfBirth,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
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

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TimeOfRegistration,Forename,Surname,DateOfBirth,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
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


        /*
         *
         *Suspension of customers
         */
         
        public async Task<ActionResult> UpdateRole(string id, string newRole, int? postId)
        {
            if (id == User.Identity.GetUserId() || newRole == null)
            {
                return RedirectToAction("Index", "Users");
            }

            ApplicationDbContext db = ApplicationDbContext.Create();

            User user = db.Users.Find(id);
            string oldRole = (user.CurrentRole);

            if (oldRole != "Customer" && newRole == "Suspended")
            {
                return RedirectToAction("Index", "Users");
            }

            await UserManager.RemoveFromRoleAsync(id, oldRole);
            await UserManager.AddToRoleAsync(id, newRole);


            if (user.CurrentRole != "Suspended")
            {
                db.Database.ExecuteSqlCommand(
                    "UPDATE AspNetUsers SET Discriminator={0} WHERE id={1}",
                    user.CurrentRole == "Admin" ? "Staff" : user.CurrentRole,
                    id);
            }

            if (postId == null)
                return RedirectToAction("Index", "Users");

            return RedirectToAction("PostDetails", "Posts", new { postId = postId });
        }
        
        /*public async Task<ActionResult> Unsuspend(string id)
        {
            if (id == User.Identity.GetUserId())
            {
                return RedirectToAction("Index", "Users");
            }

            ApplicationDbContext db = ApplicationDbContext.Create();

            User user = db.Users.Find(id);
            string oldRole = (user.CurrentRole);

            if (oldRole != "Suspended")
            {
                return RedirectToAction("Index", "Users");
            }

            await UserManager.RemoveFromRoleAsync(id, oldRole);
            await UserManager.AddToRoleAsync(id, "Customer");


            if (user.CurrentRole != "Suspended")
            {
                db.Database.ExecuteSqlCommand(
                    "UPDATE AspNetUsers SET Discriminator={0} WHERE id={1}",
                    user.CurrentRole == "Admin" ? "Staff" : user.CurrentRole,
                    id);
            }

            return RedirectToAction("Index");
        }*/


    }
}
