namespace TakinStock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRealUserName : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Users", name: "Real_user_Id", newName: "RealUser_Id");
            RenameIndex(table: "dbo.Users", name: "IX_Real_user_Id", newName: "IX_RealUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Users", name: "IX_RealUser_Id", newName: "IX_Real_user_Id");
            RenameColumn(table: "dbo.Users", name: "RealUser_Id", newName: "Real_user_Id");
        }
    }
}
