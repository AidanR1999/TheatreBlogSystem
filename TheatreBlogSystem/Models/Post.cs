using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheatreBlogSystem.Models
{
    /// <summary>
    /// Post class to hold data of each post
    /// </summary>
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DatePublished { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }

        //navigational properties
        //1:M - Post:Comment
        public virtual ICollection<Comment> Comments { get; set; }

        //1:M - Staff:Post
        [InverseProperty("Staff")]
        public string StaffId { get; set; }
        public virtual Staff Staff { get; set; }

        //1:M - Category:Post
        [InverseProperty("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}