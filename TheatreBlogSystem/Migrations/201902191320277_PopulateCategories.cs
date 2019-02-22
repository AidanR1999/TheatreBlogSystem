using System.Data.SqlClient;

namespace TheatreBlogSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategories : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (CategoryId, Name) VALUES(1, Announcement)");
            Sql("INSERT INTO Categories (CategoryId, Name) VALUES(2, Horror)");
            Sql("INSERT INTO Categories (CategoryId, Name) VALUES(3, Comedy)");
            Sql("INSERT INTO Categories (CategoryId, Name) VALUES(4, Romance)");
        }
        
        public override void Down()
        {
        }
    }
}
