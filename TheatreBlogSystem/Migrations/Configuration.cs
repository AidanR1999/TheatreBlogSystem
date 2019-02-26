namespace TheatreBlogSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TheatreBlogSystem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TheatreBlogSystem.Models.ApplicationDbContext";
        }

        protected override void Seed(TheatreBlogSystem.Models.ApplicationDbContext context)
        {
            PopulateCategories pc = new PopulateCategories();
            pc.Up();
        }
    }
}
