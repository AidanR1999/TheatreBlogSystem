using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TheatreBlogSystem.Models;
using TheatreBlogSystem.ViewModels;

namespace TheatreBlogSystem.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Category).Include(p => p.Staff);
            return View(posts.ToList());
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.StaffId = new SelectList(db.Users, "Id", "Forename");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,Body,IsApproved,DatePublished,StaffId,CategoryId")] Post post, HttpPostedFileBase fileUpload)
        {
            post.IsApproved = false;
            post.DatePublished = DateTime.Now;
            post.StaffId = User.Identity.GetUserId();

            if (fileUpload != null)
            {
                string pic = System.IO.Path.GetFileName(fileUpload.FileName);
                string path = System.IO.Path.Combine(
                    Server.MapPath("~/images/posts"), pic);
                // file is uploaded
                fileUpload.SaveAs(path);
                post.ImageLink = path;

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    fileUpload.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            ViewBag.StaffId = new SelectList(db.Users, "Id", "Forename", post.StaffId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            ViewBag.StaffId = new SelectList(db.Users, "Id", "Forename", post.StaffId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Body,IsApproved,DatePublished,ImageLink,StaffId,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            ViewBag.StaffId = new SelectList(db.Users, "Id", "Forename", post.StaffId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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

        //GET: ViewPosts
        public ActionResult ViewPosts(string categoryName)
        {
            ApplicationDbContext db = ApplicationDbContext.Create();

            PostsViewModel model = new PostsViewModel
            {
                Posts = db.Posts,
                Categories = db.Categories
            };

            if (categoryName == null)
            {
                categoryName = "All Post";
            }

            ViewBag.CategoryName = categoryName;

            return View(model);
        }

        public ActionResult PostDetails(int? postId)
        {
            Post model = new Post();
            ApplicationDbContext db = ApplicationDbContext.Create();

            if (postId == null)
            {
                return RedirectToAction("ViewPosts", "Posts");
            }

            foreach (var post in db.Posts)
            {
                if (post.PostId == postId)
                {
                    model = post;
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult PostDetails(int? postId, string comment)
        {
            Post model = new Post();
            Comment Comment = new Comment();

            ApplicationDbContext db = ApplicationDbContext.Create();

            if (postId == null)
            {
                return RedirectToAction("ViewPosts", "Posts");
            }

            foreach (var post in db.Posts)
            {
                if (post.PostId == postId)
                {
                    Comment.PostId = post.PostId;
                    Comment.Body = comment;
                    Comment.UserId = User.Identity.Name;
                    Comment.Date = DateTime.Now;
                }
            }

            if (ModelState.IsValid)
            {
                db.Comments.Add(Comment);
                db.SaveChanges();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult FilterByCategory(string filter)
        {
            return RedirectToAction("ViewPosts", "Posts", new { categoryName = filter });
        }
    }
}
