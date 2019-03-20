using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreBlogSystem.Models
{
    /// <summary>
    /// User of type staff, able to make posts
    /// </summary>
    public class Staff : User
    {
        /// <summary>
        /// initialise the the collection of posts
        /// </summary>
        public Staff()
        {
            Posts = new List<Post>();
        }

        //navigational properties
        /// <summary>
        /// holds all the posts that the staff member has made
        /// </summary>
        //1:M - Staff:Post
        public virtual ICollection<Post> Posts { get; set; }
    }
}