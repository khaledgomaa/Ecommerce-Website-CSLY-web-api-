namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.CartStatus", t => t.CartStatusId)
                .ForeignKey("dbo.Members", t => t.MemberId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.MemberId)
                .Index(t => t.CartStatusId);
            
            CreateTable(
                "dbo.CartStatus",
                c => new
                    {
                        CartStatusId = c.Int(nullable: false, identity: true),
                        CartState = c.String(),
                    })
                .PrimaryKey(t => t.CartStatusId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(nullable: false),
                        EmailId = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        CategoryId = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ProductImage = c.String(),
                        IsFeatured = c.Boolean(),
                        Amount = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Categories_DataGroupField = c.String(),
                        Categories_DataTextField = c.String(),
                        Categories_DataValueField = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.MemberRoles",
                c => new
                    {
                        MemberRoleId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberRoleId)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Shippings",
                c => new
                    {
                        ShippingId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        Adress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        OrderId = c.Int(),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ShippingId);
            
            CreateTable(
                "dbo.SlideImages",
                c => new
                    {
                        SlideImageId = c.Int(nullable: false, identity: true),
                        SlideTitle = c.String(),
                        SlideImage1 = c.String(),
                    })
                .PrimaryKey(t => t.SlideImageId);
            
            DropColumn("dbo.Categories", "IsDelete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "IsDelete", c => c.Boolean());
            DropForeignKey("dbo.MemberRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.MemberRoles", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Carts", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Carts", "CartStatusId", "dbo.CartStatus");
            DropIndex("dbo.MemberRoles", new[] { "RoleId" });
            DropIndex("dbo.MemberRoles", new[] { "MemberId" });
            DropIndex("dbo.Carts", new[] { "CartStatusId" });
            DropIndex("dbo.Carts", new[] { "MemberId" });
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropTable("dbo.SlideImages");
            DropTable("dbo.Shippings");
            DropTable("dbo.Roles");
            DropTable("dbo.MemberRoles");
            DropTable("dbo.Products");
            DropTable("dbo.Members");
            DropTable("dbo.CartStatus");
            DropTable("dbo.Carts");
        }
    }
}
