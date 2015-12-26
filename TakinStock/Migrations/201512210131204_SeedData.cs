namespace TakinStock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Real_user_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Users", "Real_user_Id");
            AddForeignKey("dbo.Users", "Real_user_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Real_user_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Users", new[] { "Real_user_Id" });
            DropColumn("dbo.Users", "Real_user_Id");
        }
    }
}
