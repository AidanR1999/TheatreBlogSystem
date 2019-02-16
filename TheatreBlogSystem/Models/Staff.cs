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
        public Staff()
        {
            Posts = new List<Post>();
        }

        //navigational properties
        //1:M - Staff:Post
        public virtual ICollection<Post> Posts { get; set; }
    }
}