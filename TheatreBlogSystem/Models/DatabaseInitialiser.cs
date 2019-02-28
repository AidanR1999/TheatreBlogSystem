using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TheatreBlogSystem.Models
{
    public class DatabaseInitialiser : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            base.Seed(context);

            if (!context.Users.Any())
            {

                //create roles
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));
                }
                if (!roleManager.RoleExists("Staff"))
                {
                    roleManager.Create(new IdentityRole("Staff"));

                }
                if (!roleManager.RoleExists("Moderator"))
                {
                    roleManager.Create(new IdentityRole("Moderator"));
                }
                if (!roleManager.RoleExists("Customer"))
                {
                    roleManager.Create(new IdentityRole("Customer"));
                }

                context.SaveChanges();

                //Create users
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));


                //Create an Admin
                if (userManager.FindByName("admin@thelocaltheatre.com") == null)
                {
                    // Super liberal password validation for password for seeds
                    userManager.PasswordValidator = new PasswordValidator
                    {
                        RequireDigit = false,
                        RequiredLength = 1,
                        RequireLowercase = false,
                        RequireNonLetterOrDigit = false,
                        RequireUppercase = false,
                    };

                    var administrator = new Staff
                    {
                        Forename = "Admin",
                        Surname = "Man",
                        UserName = "admin@thelocaltheatre.com",
                        Email = "admin@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        DateOfBirth = DateTime.Parse("02/11/1999")

                    };
                    userManager.Create(administrator, "admin123");
                    userManager.AddToRole(administrator.Id, "Admin");
                }


                //Create a moderator
                if (userManager.FindByName("manager@thelocaltheatre.com") == null)
                {
                    var manager = new Staff
                    {
                        UserName = "Moderator@thelocaltheatre.com",
                        Email = "Moderator@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        DateOfBirth = DateTime.Parse("02/11/1999")

                    };
                    userManager.Create(manager, "manager");
                    userManager.AddToRole(manager.Id, "Moderator");
                }

                // Create staff.
                if (userManager.FindByName("jeff@thelocaltheatre.com") == null)
                {

                    var jeff = new Staff
                    {
                        UserName = "jeff@thelocaltheatre.com",
                        Email = "jeff@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        DateOfBirth = DateTime.Parse("02/11/1999")
                    };

                    userManager.Create(jeff, "staff1");
                    userManager.AddToRoles(jeff.Id, "Staff");
                }

                if (userManager.FindByName("xander@thelocaltheatre.com") == null)
                {
                    var alex = new Staff
                    {
                        UserName = "xander@thelocaltheatre.com",
                        Email = "xander@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        DateOfBirth = DateTime.Parse("02/11/1999")
                    };
                    userManager.Create(alex, "staff2");
                    userManager.AddToRoles(alex.Id, "Staff");
                }

                if (userManager.FindByName("paul@thelocaltheatre.com") == null)
                {
                    var paul = new Staff
                    {
                        Forename = "Paul",
                        Surname = "Senior",
                        UserName = "Paul",
                        Email = "paul@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        DateOfBirth = DateTime.Parse("02/11/1999")
                    };
                    userManager.Create(paul, "staff3");
                    userManager.AddToRoles(paul.Id, "Staff");
                }

                //Create Customers
                if (userManager.FindByName("bill@gmail.com") == null)
                {
                    var customer = new Customer
                    {
                        UserName = "bill@gmail.com",
                        Email = "bill@gmail.com",
                        Forename = "Billy",
                        Surname = "Crow",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        IsSuspended = false,
                        DateOfBirth = DateTime.Parse("02/11/1999")

                    };
                    userManager.Create(customer, "customer1");
                    userManager.AddToRole(customer.Id, "Customer");
                }


                if (userManager.FindByName("bob@gmail.com") == null)
                {
                    var bob = new Customer
                    {
                        UserName = "bob@gmail.com",
                        Email = "bob@gmail.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        Forename = "Bob",
                        Surname = "Williams",
                        IsSuspended = false,
                        DateOfBirth = DateTime.Parse("02/11/1999")
                    };
                    userManager.Create(bob, "customer2");
                    userManager.AddToRoles(bob.Id, "Customer");
                }

                if (userManager.FindByName("steveb@gmail.com") == null)
                {
                    var steve = new Customer
                    {
                        UserName = "Steve",
                        Email = "steveb@gmail.com",
                        EmailConfirmed = true,
                        Forename = "Steve",
                        Surname = "Fist",
                        TimeOfRegistration = DateTime.Now,
                        IsSuspended = false,
                        DateOfBirth = DateTime.Parse("02/11/1999")
                    };
                    userManager.Create(steve, "customer3");
                    userManager.AddToRoles(steve.Id, "Customer");
                }
                if (userManager.FindByName("gary@gmail.com") == null)
                {
                    var gary = new Customer
                    {
                        UserName = "gary@gmail.com",
                        Email = "gary@gmail.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        Forename = "Garry",
                        Surname = "Hugh",
                        IsSuspended = false,
                        DateOfBirth = DateTime.Parse("02/11/1999")
                    };
                    userManager.Create(gary, "customer4");
                    userManager.AddToRoles(gary.Id, "Customer");
                }

                //create categories
                Category Announcement = new Category
                {
                    Name = "Announcement"
                };
                context.Categories.Add(Announcement);

                Category Comedy = new Category
                {
                    Name = "Comedy"
                };
                context.Categories.Add(Comedy);

                Category Horror = new Category
                {
                    Name = "Horror"
                };
                context.Categories.Add(Horror);

                Category Romance = new Category
                {
                    Name = "Romance"
                };
                context.Categories.Add(Romance);

                context.SaveChanges();
            }

           // context.SaveChanges();


            



        }//end method

           
        }//end class


    }//end namespace
