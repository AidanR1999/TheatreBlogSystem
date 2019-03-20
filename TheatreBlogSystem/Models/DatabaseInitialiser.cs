    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TheatreBlogSystem.Models
{
    /// <summary>
    /// seeds the database with roles, users, categories and posts
    /// </summary>
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
                if (!roleManager.RoleExists("Suspended"))
                {
                    roleManager.Create(new IdentityRole("Suspended"));
                }

                context.SaveChanges();

                //Create users
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));


                //Create an Admin
                if (userManager.FindByName("theatreadmin@thelocaltheatre.com") == null)
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
                        Forename = "Aidan",
                        Surname = "Rooney",
                        UserName = "theatreadmin@thelocaltheatre.com",
                        Email = "theatreadmin@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        DateOfBirth = DateTime.Parse("02/11/1999")

                    };
                    userManager.Create(administrator, "admin123");
                    userManager.AddToRole(administrator.Id, "Admin");
                }


                //Create a moderator
                if (userManager.FindByName("theatremod@thelocaltheatre.com") == null)
                {
                    var mod = new Staff
                    {
                        Forename = "Mike",
                        Surname = "Gunn",
                        UserName = "theatremod@thelocaltheatre.com",
                        Email = "theatremod@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        DateOfBirth = DateTime.Parse("31/5/1995")

                    };
                    userManager.Create(mod, "mod123");
                    userManager.AddToRole(mod.Id, "Moderator");
                }

                // Create staff.
                if (userManager.FindByName("danny@thelocaltheatre.com") == null)
                {

                    var staff1 = new Staff
                    {
                        Forename = "Danny",
                        Surname = "Carswell",
                        UserName = "danny@thelocaltheatre.com",
                        Email = "danny@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        DateOfBirth = DateTime.Parse("19/03/1999")
                    };

                    userManager.Create(staff1, "staff1");
                    userManager.AddToRoles(staff1.Id, "Staff");
                }

                if (userManager.FindByName("jamie@thelocaltheatre.com") == null)
                {
                    var staff2 = new Staff
                    {
                        Forename = "Jamie",
                        Surname = "Lawn",
                        UserName = "jamie@thelocaltheatre.com",
                        Email = "jamie@thelocaltheatre.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        DateOfBirth = DateTime.Parse("28/09/1990")
                    };
                    userManager.Create(staff2, "staff2");
                    userManager.AddToRoles(staff2.Id, "Staff");
                }


                //Create Customers
                if (userManager.FindByName("Mork@gmail.com") == null)
                {
                    var customer1 = new Customer
                    {
                        Forename = "Mork",
                        Surname = "McNulty",
                        UserName = "Mork@gmail.com",
                        Email = "Mork@gmail.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        IsSuspended = false,
                        DateOfBirth = DateTime.Parse("10/06/1994")

                    };
                    userManager.Create(customer1, "customer1");
                    userManager.AddToRole(customer1.Id, "Customer");
                }


                if (userManager.FindByName("Ewan@gmail.com") == null)
                {
                    var customer2 = new Customer
                    {
                        Forename = "Ewan",
                        Surname = "MacPhearson",
                        UserName = "Ewan@gmail.com",
                        Email = "Ewan@gmail.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        IsSuspended = false,
                        DateOfBirth = DateTime.Parse("17/07/1998")
                    };
                    userManager.Create(customer2, "customer2");
                    userManager.AddToRoles(customer2.Id, "Customer");
                }

                //Create Suspended
                if (userManager.FindByName("div@gmail.com") == null)
                {
                    var suspended1 = new Customer
                    {
                        Forename = "Div",
                        Surname = "Campbell",
                        UserName = "div@gmail.com",
                        Email = "div@gmail.com",
                        TimeOfRegistration = DateTime.Now,
                        EmailConfirmed = true,
                        IsSuspended = true,
                        DateOfBirth = DateTime.Parse("26/05/1992")
                    };
                    userManager.Create(suspended1, "suspended1");
                    userManager.AddToRoles(suspended1.Id, "Suspended");
                }

                //Create Categories
                Category Announcement = new Category
                {
                    Name = "Announcement"
                };
                context.Categories.Add(Announcement);

                Category Film = new Category
                {
                    Name = "Film Review"
                };
                context.Categories.Add(Film);

                Category Play = new Category
                {
                    Name = "Play Review"
                };
                context.Categories.Add(Play);

                
                //Create Posts
                //announcements
                Post Announcement1 = new Post
                {
                    Title = "Proin iaculis, tellus sed feugiat feugiat",
                    Body = "Lectus urna sollicitudin turpis, molestie molestie mauris nisi vitae enim. Sed tempor nibh tortor, ut pretium diam hendrerit eu. Pellentesque porttitor a est ut ultricies. Nam ullamcorper tempor urna, non gravida velit rhoncus in. Pellentesque bibendum, orci sed faucibus vestibulum, nibh lectus aliquam sem, tempor aliquam felis libero eget risus. Praesent non tincidunt ipsum. Sed sollicitudin lacinia porta. Ut tincidunt rhoncus dui, at iaculis ligula ornare vitae. In ut tortor condimentum tortor finibus consectetur sed blandit metus. Vestibulum ultricies interdum ex sit amet placerat. Nam ullamcorper est vel lorem aliquam auctor.",
                    IsApproved = true,
                    DatePublished = DateTime.Now,
                    ImageLink = "/Content/img/architecture-auditorium-chairs-109669.jpg",
                    CategoryId = 1,
                    StaffId = userManager.FindByEmail("jamie@thelocaltheatre.com").Id
                };
                context.Posts.Add(Announcement1);

                Post Announcement2 = new Post
                {
                    Title = "Suspendisse non finibus erat",
                    Body = "Praesent pharetra nisl id diam pharetra condimentum. Donec metus nisl, viverra at commodo eget, interdum eu mauris. Maecenas posuere elit nec magna aliquet, in dapibus sapien pharetra. Maecenas non vulputate urna. Nam volutpat justo eu dictum scelerisque. Etiam convallis lectus nec arcu tempus, lacinia volutpat dolor finibus. Quisque eget ultrices velit. Morbi interdum tincidunt justo eu convallis. Curabitur sit amet lectus metus. Nam eu pulvinar felis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. ",
                    IsApproved = true,
                    DatePublished = DateTime.Now,
                    ImageLink = "/Content/img/architecture-auditorium-chairs-109669.jpg",
                    CategoryId = 1,
                    StaffId = userManager.FindByEmail("theatremod@thelocaltheatre.com").Id
                };
                context.Posts.Add(Announcement2);

                //film reviews
                Post film1 = new Post
                {
                    Title = "Nullam mi lacus",
                    Body = "Rhoncus quis egestas id, egestas a purus. Pellentesque tincidunt libero nec tempor lobortis. Suspendisse potenti. Sed sit amet lectus ornare, posuere magna ac, tempus risus. Etiam placerat luctus libero id consequat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed auctor tristique consectetur. Vivamus cursus urna nec hendrerit semper. Praesent congue lorem porttitor nisi placerat sodales. Vivamus ac eros arcu. Morbi commodo sapien et nisi blandit, in elementum nunc aliquam. Donec in risus porta, efficitur orci nec, malesuada orci. Nulla efficitur id erat et luctus. ",
                    IsApproved = true,
                    DatePublished = DateTime.Now,
                    ImageLink = "/Content/img/architecture-auditorium-chairs-109669.jpg",
                    CategoryId = 2,
                    StaffId = userManager.FindByEmail("jamie@thelocaltheatre.com").Id
                };
                context.Posts.Add(film1);

                //play reviews
                Post play1 = new Post
                {
                    Title = "Sed consequat",
                    Body = "Enim quis rhoncus hendrerit, sem quam tincidunt nisl, ut sollicitudin est tellus sed libero. Ut elementum tincidunt sem feugiat iaculis. Fusce dapibus odio eget neque accumsan, quis suscipit nunc rutrum. Nullam id sagittis enim, id malesuada turpis. Donec ipsum dui, volutpat in magna quis, tempus condimentum nulla. Aenean non nisl faucibus, laoreet erat laoreet, bibendum orci. Pellentesque ac neque vitae ligula efficitur feugiat. Vivamus luctus sit amet massa ut dictum. Donec facilisis diam vel mi fermentum lacinia. Nam eu enim vel mi congue pretium ac ac mauris. Nulla nec arcu cursus, pretium metus eu, suscipit leo. Sed bibendum ipsum metus, et scelerisque magna lobortis ut. ",
                    IsApproved = true,
                    DatePublished = DateTime.Now,
                    ImageLink = "/Content/img/architecture-auditorium-chairs-109669.jpg",
                    CategoryId = 3,
                    StaffId = userManager.FindByEmail("jamie@thelocaltheatre.com").Id
                };
                context.Posts.Add(play1);

                Post play2 = new Post
                {
                    Title = "Nunc posuere arcu lectus",
                    Body = "Quis cursus nisi egestas non. Integer non mauris placerat quam semper condimentum. Proin dapibus ut sapien et ultricies. Suspendisse pulvinar orci quam. Sed a venenatis purus, ut consequat libero. Donec tincidunt, massa accumsan pulvinar dictum, nunc libero feugiat erat, id porta nisi enim eget turpis. Nullam eu lacus elementum tortor pretium congue id sagittis nibh. Vivamus a egestas nisi. Donec maximus lacus leo. ",
                    IsApproved = true,
                    DatePublished = DateTime.Now,
                    ImageLink = "/Content/img/architecture-auditorium-chairs-109669.jpg",
                    CategoryId = 3,
                    StaffId = userManager.FindByEmail("danny@thelocaltheatre.com").Id
                };
                context.Posts.Add(play2);

                //non-approved posts
                Post nonapproved1 = new Post
                {
                    Title = "Vestibulum id est nibh",
                    Body = "Nam cursus justo quis fermentum aliquet. Etiam mollis tempor hendrerit. Nullam auctor sed eros ut varius. Sed pellentesque erat nibh. Cras sed mattis velit. Etiam tempor, ex sit amet maximus tincidunt, lectus ante molestie libero, ultricies lacinia dui tellus vitae nunc. ",
                    IsApproved = false,
                    DatePublished = DateTime.Now,
                    ImageLink = "/Content/img/architecture-auditorium-chairs-109669.jpg",
                    CategoryId = 2,
                    StaffId = userManager.FindByEmail("danny@thelocaltheatre.com").Id
                };
                context.Posts.Add(nonapproved1);

                Post nonapproved2 = new Post
                {
                    Title = "Cras vehicula vestibulum luctus",
                    Body = "Vivamus eleifend iaculis nunc at auctor. Maecenas tincidunt porta risus commodo finibus. Morbi dignissim tempus ex, sed euismod quam efficitur a. Mauris ante massa, bibendum eget tincidunt sit amet, semper eget metus. Sed eu molestie purus. Morbi rutrum ultrices fringilla. Aliquam viverra felis interdum felis blandit placerat. Nullam commodo dolor sit amet metus luctus semper. Morbi at mauris rhoncus, venenatis ipsum cursus, pretium felis. ",
                    IsApproved = false,
                    DatePublished = DateTime.Now,
                    ImageLink = "/Content/img/architecture-auditorium-chairs-109669.jpg",
                    CategoryId = 1,
                    StaffId = userManager.FindByEmail("jamie@thelocaltheatre.com").Id
                };
                context.Posts.Add(nonapproved2);

                context.SaveChanges();
            }

        }           
    }
}
