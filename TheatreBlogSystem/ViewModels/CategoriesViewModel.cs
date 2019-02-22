using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheatreBlogSystem.Models;

namespace TheatreBlogSystem.ViewModels
{
    public class CategoriesViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public CategoriesViewModel()
        {
            Categories = new List<Category>();
        }
    }
}