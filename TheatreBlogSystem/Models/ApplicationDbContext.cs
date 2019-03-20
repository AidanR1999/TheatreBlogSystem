using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TheatreBlogSystem.Models
{
    /// <summary>
    /// the context for the database
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// gives access to the categories table
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// gives access to the posts table
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// gives access to the comments table
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

        /// <summary>
        /// sets the initialiser of the database
        /// </summary>
        public ApplicationDbContext()
            : base("TheatreDBConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseInitialiser());
        }

        /// <summary>
        /// creates an instance of the database
        /// </summary>
        /// <returns>Context</returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
