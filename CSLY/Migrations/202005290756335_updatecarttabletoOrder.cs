namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecarttabletoOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "CartStatusId", "dbo.CartStatus");
            DropForeignKey("dbo.Carts", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropIndex("dbo.Carts", new[] { "MemberId" });
            DropIndex("dbo.Carts", new[] { "CartStatusId" });
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        MemberId = c.Int(),
                        CartStatusId = c.Int(),
                    })
                .PrimaryKey(t => t.CartId);
            
            DropTable("dbo.Orders");
            CreateIndex("dbo.Carts", "CartStatusId");
            CreateIndex("dbo.Carts", "MemberId");
            CreateIndex("dbo.Carts", "ProductId");
            AddForeignKey("dbo.Carts", "ProductId", "dbo.Products", "ProductId");
            AddForeignKey("dbo.Carts", "MemberId", "dbo.Members", "MemberId");
            AddForeignKey("dbo.Carts", "CartStatusId", "dbo.CartStatus", "CartStatusId");
        }
    }
}
