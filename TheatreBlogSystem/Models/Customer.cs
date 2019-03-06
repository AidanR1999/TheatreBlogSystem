using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreBlogSystem.Models
{
    /// <summary>
    /// User of type customer, is able to be suspended from making comments
    /// </summary>
    public class Customer : User
    {
        public bool IsSuspended { get; set; }
    }
}