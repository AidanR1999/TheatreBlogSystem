using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreBlogSystem.Models;

namespace TheatreBlogSystem.ViewModels
{
    public class PostsViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        public PostsViewModel()
        {
            Posts = new List<Post>();
        }
    }
}