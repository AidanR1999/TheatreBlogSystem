using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreBlogSystem.Models;

namespace TheatreBlogSystem.ViewModels
{
    public class PostsCategoryViewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DatePublished { get; set; }
        public string CategoryName { get; set; }
    }
}