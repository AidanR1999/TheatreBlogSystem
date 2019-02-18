using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TheatreBlogSystem.Models
{
    public class DatabaseInitialiser:DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);

            if (!context.Users.Any())
            {

                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));
                }
                if (!roleManager.RoleExists("Staff"))
                {
                    roleManager.Create(new IdentityRole("Staff"));

                }
                if (!roleManager.RoleExists("Manager"))
                {
                    roleManager.Create(new IdentityRole("Manager"));
                }
                if (!roleManager.RoleExists("Customer"))
                {
                    roleManager.Create(new IdentityRole("Customer"));
                }
                if (!roleManager.RoleExists("Suspended"))
                {
                    roleManager.Create(new IdentityRole("Suspended"));
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
                        UserName = "admin@thelocaltheatre.com",
                        Email = "admin@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true

                    };
                    userManager.Create(administrator, "admin123");
                    userManager.AddToRole(administrator.Id, "Admin");
                }


                //Create a manager
                if (userManager.FindByName("manager@thelocaltheatre.com") == null)
                {
                    var manager = new Staff
                    {
                        UserName = "manager@thelocaltheatre.com",
                        Email = "manager@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true

                    };
                    userManager.Create(manager, "manager");
                    userManager.AddToRole(manager.Id, "Manager");
                }

                // Create staff.
                if (userManager.FindByName("jeff@thelocaltheatre.com") == null)
                {

                    var jeff = new Staff
                    {
                        UserName = "jeff@thelocaltheatre.com",
                        Email = "jeff@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true
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
                    };
                    userManager.Create(alex, "staff2");
                    userManager.AddToRoles(alex.Id, "Staff");
                }

                if (userManager.FindByName("paul@thelocaltheatre.com") == null)
                {
                    var paul = new Staff
                    {
                        UserName = "Paul",
                        Email = "paul@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
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
                        IsSuspended = false

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
                        IsSuspended = false
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
                        IsSuspended = false
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
                        IsSuspended = false
                    };
                    userManager.Create(gary, "customer4");
                    userManager.AddToRoles(gary.Id, "Customer");
                }

                //Create a few suspended customers
                if (userManager.FindByName("bill@gmail.com") == null)
                {
                    var bill = new Customer
                    {
                        UserName = "bill@gmail.com",
                        Email = "bill@gmail.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        Forename = "Bill",
                        Surname = "Black",
                        IsSuspended = true
                    };
                    userManager.Create(bill, "suspended1");
                    userManager.AddToRoles(bill.Id, "Suspended");
                }

                if (userManager.FindByName("greg@gmail.com") == null)
                {



                    var greg = new Customer
                    {
                        UserName = "greg@gmail.com",
                        Email = "greg@gmail.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        Forename = "Greg",
                        Surname = "Grey",
                        IsSuspended = true
                    };
                    userManager.Create(greg, "suspended2");
                    userManager.AddToRoles(greg.Id, "Suspended");
                }
                context.SaveChanges();
            }

           // context.SaveChanges();



        }//end method

           
        }//end class


    }//end namespace
