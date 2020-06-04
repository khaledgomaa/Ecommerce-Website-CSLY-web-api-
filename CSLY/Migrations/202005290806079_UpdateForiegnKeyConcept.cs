namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateForiegnKeyConcept : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ShippingId = c.Int(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            AddColumn("dbo.Orders", "ClientId_Id", c => c.Int());
            AddColumn("dbo.Shippings", "ClientId_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ClientId_Id");
            CreateIndex("dbo.Shippings", "ClientId_Id");
            AddForeignKey("dbo.Orders", "ClientId_Id", "dbo.AccountInfoes", "Id");
            AddForeignKey("dbo.Shippings", "ClientId_Id", "dbo.AccountInfoes", "Id", cascadeDelete: true);
            DropColumn("dbo.Orders", "ClientId");
            DropColumn("dbo.Shippings", "ClientId");
            DropTable("dbo.CartStatus");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartStatus",
                c => new
                    {
                        CartStatusId = c.Int(nullable: false, identity: true),
                        CartState = c.String(),
                    })
                .PrimaryKey(t => t.CartStatusId);
            
            AddColumn("dbo.Shippings", "ClientId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ClientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Shippings", "ClientId_Id", "dbo.AccountInfoes");
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientId_Id", "dbo.AccountInfoes");
            DropIndex("dbo.Shippings", new[] { "ClientId_Id" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            DropIndex("dbo.Orders", new[] { "ClientId_Id" });
            DropColumn("dbo.Shippings", "ClientId_Id");
            DropColumn("dbo.Orders", "ClientId_Id");
            DropTable("dbo.OrderItems");
        }
    }
}
