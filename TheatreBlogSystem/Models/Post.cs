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
        /// <summary>
        /// holds the id of the post
        /// </summary>
        [Key]
        public int PostId { get; set; }

        /// <summary>
        /// holds the post title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// holds the content of the post
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// holds if the post is approved or not
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// holds the date and time of publishing the post
        /// </summary>
        public DateTime DatePublished { get; set; }

        /// <summary>
        /// holds the image associated with the post
        /// </summary>
        public string ImageLink { get; set; }

        /// <summary>
        /// instantiate the collection of comments
        /// </summary>
        public Post()
        {
            Comments = new List<Comment>();
        }

        //navigational properties
        /// <summary>
        /// holds a list of comments that are related to the post
        /// </summary>
        //1:M - Post:Comment
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// holds the info related to the author of the post
        /// </summary>
        //1:M - Staff:Post
        [InverseProperty("Staff")]
        public string StaffId { get; set; }
        public virtual Staff Staff { get; set; }

        /// <summary>
        /// holds the info related to the category of the post
        /// </summary>
        //1:M - Category:Post
        [InverseProperty("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}