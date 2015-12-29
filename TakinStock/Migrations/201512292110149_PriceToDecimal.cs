namespace TakinStock.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "PurchasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "PurchasePrice", c => c.String());
        }
    }
}
