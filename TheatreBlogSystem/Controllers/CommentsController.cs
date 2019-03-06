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
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Post).Include(c => c.User);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
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
        [Authorize(Roles = "Admin, Moderator, Staff, Customer")]
        public ActionResult Create()
        {
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Forename");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Comments/Delete/5
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
        public PartialViewResult _ViewComments(ICollection<Comment> comments)
        {
            return PartialView(comments);
        }

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
