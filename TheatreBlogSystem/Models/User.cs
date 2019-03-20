using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace TheatreBlogSystem.Models
{
    /// <summary>
    /// User class, inherits from IdentityUser class
    /// Holds extra information about the user
    /// </summary>
    public abstract class User : IdentityUser
    {
        /// <summary>
        /// holds the time and date of the users registration
        /// DateTime
        /// </summary>
        public DateTime? TimeOfRegistration { get; set; }

        /// <summary>
        /// holds the user's first name
        /// String
        /// </summary>
        public string Forename { get; set; }

        /// <summary>
        /// holds the user's last name
        /// String
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// holds the user's birth date
        /// DateTime
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        
        public User()
        {
            Comments = new List<Comment>();
        }

        //navigational properties
        /// <summary>
        /// holds the comments that the user has made
        /// </summary>
        //1:M - User:Comment
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// get the identity information about the user
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// allows access to user manager
        /// </summary>
        private ApplicationUserManager userManager;

        /// <summary>
        /// gets the current role of a user from the user manager
        /// </summary>
        [NotMapped]
        public string CurrentRole
        {
            get
            {
                if (userManager == null)
                {
                    userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

                }

                return userManager.GetRoles(Id).Single();
            }
        }

    }
}