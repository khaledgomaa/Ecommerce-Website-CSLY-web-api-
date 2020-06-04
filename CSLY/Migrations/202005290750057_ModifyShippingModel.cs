namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyShippingModel : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Shippings");
            DropColumn("dbo.Shippings", "ShippingId");
            DropColumn("dbo.Shippings", "MemberId");
            DropColumn("dbo.Shippings", "Adress");
            DropColumn("dbo.Shippings", "City");
            DropColumn("dbo.Shippings", "OrderId");
            DropColumn("dbo.Shippings", "AmountPaid");
            AddColumn("dbo.Shippings", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Shippings", "ClientId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Shippings", "Id");
           
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shippings", "AmountPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Shippings", "OrderId", c => c.Int());
            AddColumn("dbo.Shippings", "City", c => c.String(nullable: false));
            AddColumn("dbo.Shippings", "Adress", c => c.String(nullable: false));
            AddColumn("dbo.Shippings", "MemberId", c => c.Int(nullable: false));
            AddColumn("dbo.Shippings", "ShippingId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Shippings");
            DropColumn("dbo.Shippings", "ClientId");
            DropColumn("dbo.Shippings", "Id");
            AddPrimaryKey("dbo.Shippings", "ShippingId");
        }
    }
}
