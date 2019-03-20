using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreBlogSystem.Models;

namespace TheatreBlogSystem.ViewModels
{
    /// <summary>
    /// the view model for the View Posts view
    /// </summary>
    public class PostsViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public PostsViewModel()
        {
            Posts = new List<Post>();
            Categories = new List<Category>();
        }
    }
}