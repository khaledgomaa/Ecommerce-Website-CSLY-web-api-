namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductAndSlideImageModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SlideImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "SlideImageId");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "IsFeatured", c => c.Boolean());
            AddColumn("dbo.Products", "ProductImage", c => c.String());
           
        }
    }
}
