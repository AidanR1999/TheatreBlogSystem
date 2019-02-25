using System.Data.SqlClient;

namespace TheatreBlogSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Name) VALUES('Announcement')");
            Sql("INSERT INTO Categories (Name) VALUES('Horror')");
            Sql("INSERT INTO Categories (Name) VALUES('Comedy')");
            Sql("INSERT INTO Categories (Name) VALUES('Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
