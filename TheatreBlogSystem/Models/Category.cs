using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TheatreBlogSystem.Models
{
    /// <summary>
    /// Category class used to give a category for every post
    /// </summary>
    public class Category
    {
        /// <summary>
        /// holds the id of the category
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// holds the name of the category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// initialises the collection of posts
        /// </summary>
        public Category()
        {
            Posts = new List<Post>();
        }

        //navigational properties
        /// <summary>
        /// holds all the posts that are in the category
        /// </summary>
        //1:M - Category:Post
        public virtual ICollection<Post> Posts { get; set; }
    }
}