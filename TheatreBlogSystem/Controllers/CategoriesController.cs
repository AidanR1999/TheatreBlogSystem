using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreBlogSystem.Models;
using TheatreBlogSystem.ViewModels;

namespace TheatreBlogSystem.Controllers
{
    /// <summary>
    /// The main handler for actions that relate to categories
    /// </summary>
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        /// <summary>
        /// loads the categories index page
        /// </summary>
        /// <returns>Categories Index Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        /// <summary>
        /// gets the selected category and loads its details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category Details Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create7
        /// <summary>
        /// loads the categories create page
        /// </summary>
        /// <returns>Categories Create Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        /// <summary>
        /// gets the data from the categories create page and adds the category to the database
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Categories Index Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        /// <summary>
        /// gets the selected category and loads the categories edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Categories Edit Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        /// <summary>
        /// gets the data from the categories edit page and updates the category in the database
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Categories Index Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        /// <summary>
        /// gets the selected category and loads the categories delete page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Categories Delete Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        /// <summary>
        /// deletes the selected category from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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

       
    }
}
