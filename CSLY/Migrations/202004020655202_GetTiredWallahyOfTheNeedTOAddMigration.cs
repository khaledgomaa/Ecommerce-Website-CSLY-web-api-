namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GetTiredWallahyOfTheNeedTOAddMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "SlideImageId", "dbo.SlideImages");
            DropIndex("dbo.Products", new[] { "SlideImageId" });
            AddColumn("dbo.Products", "ImagePath", c => c.String());
            DropColumn("dbo.Products", "SlideImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "SlideImageId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "ImagePath");
            CreateIndex("dbo.Products", "SlideImageId");
            AddForeignKey("dbo.Products", "SlideImageId", "dbo.SlideImages", "SlideImageId", cascadeDelete: true);
        }
    }
}
