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
        /// <summary>
        /// holds whether or not a customer is suspended
        /// </summary>
        public bool IsSuspended { get; set; }
    }
}