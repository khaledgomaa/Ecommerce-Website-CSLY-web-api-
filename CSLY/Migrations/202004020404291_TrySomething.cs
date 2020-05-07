namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrySomething : DbMigration
    {
        public override void Up()
        {

            DropColumn("dbo.Roles", "RoleName");
            DropColumn("dbo.SlideImages", "SlideImage1");
            DropColumn("dbo.Products", "IsFeatured");
            DropColumn("dbo.Products", "ProductImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SlideImages", "SlideImage1", c => c.String());
            AddColumn("dbo.Roles", "RoleName", c => c.String());
            AddColumn("dbo.Products", "IsFeatured", c => c.String());
            AddColumn("dbo.Products", "ProductImage", c => c.String());

        }
    }
}
