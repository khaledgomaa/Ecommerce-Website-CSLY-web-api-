namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContinueUpdatingProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "SlideImageId", "dbo.SlideImages");
            DropIndex("dbo.Products", new[] { "SlideImageId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Products", "SlideImageId");
            AddForeignKey("dbo.Products", "SlideImageId", "dbo.SlideImages", "SlideImageId", cascadeDelete: true);
        }
    }
}
