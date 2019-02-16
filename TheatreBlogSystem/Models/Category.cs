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
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public Category()
        {
            Posts = new List<Post>();
        }

        //navigational properties
        //1:M - Category:Post
        public virtual ICollection<Post> Posts { get; set; }
    }
}