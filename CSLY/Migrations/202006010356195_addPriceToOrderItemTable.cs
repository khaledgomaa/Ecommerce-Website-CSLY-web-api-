namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPriceToOrderItemTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "Price");
        }
    }
}
