using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TheatreBlogSystem.Models;

namespace TheatreBlogSystem.Controllers
{
    /// <summary>
    /// The main handler for actions that relate to comments
    /// </summary>
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        /// <summary>
        /// loads the comments index page, inaccessible through normal use
        /// </summary>
        /// <returns>Comments Index Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Post).Include(c => c.User);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        /// <summary>
        /// gets the selected comment and loads the details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Comment Details Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        /// <summary>
        /// loads the comments create page
        /// </summary>
        /// <returns>Comments Create Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff, Customer")]
        public ActionResult Create()
        {
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Forename");
            return View();
        }

        // POST: Comments/Create
        /// <summary>
        /// gets the data from the comments create page and adds the comment to the database
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>Comments Index Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Date,Body,UserId,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Title", comment.PostId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Forename", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        /// <summary>
        /// gets the selected comment and loads the comments edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Comments Edit Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Title", comment.PostId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Forename", comment.UserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        /// <summary>
        /// gets the data from the comments edit page and updates the comment in the database
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>Comments Index Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Date,Body,UserId,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Title", comment.PostId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Forename", comment.UserId);
            return View(comment);
        }

        /// <summary>
        /// deletes the selected comment from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View Posts Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("ViewPosts", "Posts", null);
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
        /// adds a comment to the database
        /// </summary>
        /// <param name="commentBody"></param>
        /// <param name="postId"></param>
        /// <returns>Post Details Page</returns>
        [Authorize(Roles = "Admin, Moderator, Staff, Customer")]
        [HttpPost]
        public ActionResult MakeComment(string commentBody, int? postId)
        {
            Comment comment = new Comment();
            comment.Body = commentBody;
            comment.Date = DateTime.Now;
            comment.UserId = User.Identity.GetUserId();
            if (postId == null)
                RedirectToAction("ViewPosts", "Posts");

            comment.PostId = (int) postId;

            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("PostDetails", "Posts", new { postId });
        }

        //GET: ViewComments PartialView
        /// <summary>
        /// display approved comments in the post details page
        /// </summary>
        /// <param name="comments"></param>
        /// <returns>View Comments Partial View</returns>
        public PartialViewResult _ViewComments(ICollection<Comment> comments)
        {
            return PartialView(comments);
        }

        /// <summary>
        /// allows admins and moderators to approve selected comments
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="approved"></param>
        /// <returns>Post Details Page</returns>
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult ApproveComment(int? commentId, bool approved)
        {
            if(commentId == null)
                RedirectToAction("ViewPosts", "Posts");

            ApplicationDbContext db = new ApplicationDbContext();
            Comment comment = db.Comments.Find(commentId);

            if(comment == null)
                RedirectToAction("ViewPosts", "Posts");

            comment.CommentIsApproved = approved;

            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("PostDetails", "Posts", new { postId = comment.PostId });
        }
    }
}
