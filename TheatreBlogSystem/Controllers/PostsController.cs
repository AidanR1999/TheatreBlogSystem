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
        /// <summary>
        /// loads the posts index page, inaccessible from normal interaction 
        /// </summary>
        /// <returns>Post Index Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Category).Include(p => p.Staff);
            return View(posts.ToList());
        }

        // GET: Posts/Create
        /// <summary>
        /// loads the create post page
        /// </summary>
        /// <returns>Create post page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.StaffId = new SelectList(db.Users, "Id", "Forename");
            return View();
        }

        // POST: Posts/Create
        /// <summary>
        /// gets the data from the create posts page and adds the post to the database
        /// </summary>
        /// <param name="post"></param>
        /// <returns>View Posts Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,Body,IsApproved,DatePublished,StaffId,CategoryId")] Post post)
        {
            post.ImageLink = "/Content/img/architecture-auditorium-chairs-109669.jpg";
            post.IsApproved = false;
            post.DatePublished = DateTime.Now;
            post.StaffId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("ViewPosts");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            ViewBag.StaffId = new SelectList(db.Users, "Id", "Forename", post.StaffId);
            return View(post);
        }

        // GET: Posts/Edit/5
        /// <summary>
        /// load the edit post page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit post page</returns>
        [Authorize(Roles = "Admin, Moderator")]
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
        /// <summary>
        /// gets the data from the edit posts page and updates the post in the database with the new information
        /// </summary>
        /// <param name="post"></param>
        /// <returns>View Posts Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Body,IsApproved,DatePublished,ImageLink,StaffId,CategoryId")] Post post)
        {
            post.IsApproved = false;

            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewPosts");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            ViewBag.StaffId = new SelectList(db.Users, "Id", "Forename", post.StaffId);
            return RedirectToAction("Edit");
        }

        // GET: Posts/Delete/5
        /// <summary>
        /// loads the delete post page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete Posts Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
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
        /// <summary>
        /// removes the post from the database, including all comments
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View Posts Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("ViewPosts");
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
        /// <summary>
        /// displays the posts to the user
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>View Posts Page</returns>
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

        /// <summary>
        /// gets the selected post and loads the details of the post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>Post Details Page</returns>
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
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

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult ApprovePost(int? postId)
        {
            if(postId == null)
                return RedirectToAction("ViewPosts", "Posts");

            ApplicationDbContext db = ApplicationDbContext.Create();
            Post post = db.Posts.Find(postId);

            if(post == null)
                return RedirectToAction("ViewPosts", "Posts");

            post.IsApproved = true;

            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ViewPosts", "Posts");
        }

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DisapprovePost(int? postId)
        {
            if (postId == null)
                return RedirectToAction("ViewPosts", "Posts");

            ApplicationDbContext db = ApplicationDbContext.Create();
            Post post = db.Posts.Find(postId);

            if (post == null)
                return RedirectToAction("ViewPosts", "Posts");

            post.IsApproved = false;

            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ViewPosts", "Posts");
        }
    }
}
