using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheatreBlogSystem.Models
{
    /// <summary>
    /// Comment class to hold the data about comments
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// holds the id of the comment
        /// </summary>
        [Key]
        public int CommentId { get; set; }

        /// <summary>
        /// holds the date and time the comment was posted
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// holds the content of the comment
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// holds whether or not the comment has been approved
        /// </summary>
        public bool CommentIsApproved { get; set; }
            
        //navigational properties
        /// <summary>
        /// holds the author of the comments
        /// </summary>
        //1:M - User:Comment
        [InverseProperty("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        //1:M - Post:Comment
        /// <summary>
        /// holds the post the comment was made to
        /// </summary>
        [InverseProperty("Post")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}