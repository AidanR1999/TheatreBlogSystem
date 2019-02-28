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
        [Key]
        public int CommentId { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
        public bool CommentIsApproved { get; set; }

        public Comment()
        {

        }
            
        //navigational properties
        //1:M - User:Comment
        [InverseProperty("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        //1:M - Post:Comment
        [InverseProperty("Post")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}