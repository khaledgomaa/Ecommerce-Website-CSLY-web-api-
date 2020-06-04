namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateForiegnKeysForSomeTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientId_Id", "dbo.AccountInfoes");
            DropIndex("dbo.Orders", new[] { "ClientId_Id" });
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            RenameColumn(table: "dbo.OrderItems", name: "Order_Id", newName: "OrderId");
            RenameColumn(table: "dbo.Orders", name: "ClientId_Id", newName: "ClientId");
            AlterColumn("dbo.Orders", "ClientId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ClientId");
            CreateIndex("dbo.OrderItems", "OrderId");
            AddForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ClientId", "dbo.AccountInfoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ClientId", "dbo.AccountInfoes");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            AlterColumn("dbo.OrderItems", "OrderId", c => c.Int());
            AlterColumn("dbo.Orders", "ClientId", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "ClientId", newName: "ClientId_Id");
            RenameColumn(table: "dbo.OrderItems", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.OrderItems", "Order_Id");
            CreateIndex("dbo.Orders", "ClientId_Id");
            AddForeignKey("dbo.Orders", "ClientId_Id", "dbo.AccountInfoes", "Id");
            AddForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
