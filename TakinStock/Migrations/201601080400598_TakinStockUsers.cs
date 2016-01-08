namespace TakinStock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TakinStockUsers : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "TakinStockUsers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TakinStockUsers", newName: "Users");
        }
    }
}
