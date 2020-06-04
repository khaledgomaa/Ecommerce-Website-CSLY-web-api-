namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteShippingIdFromOrderItemTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderItems", "ShippingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "ShippingId", c => c.Int(nullable: false));
        }
    }
}
