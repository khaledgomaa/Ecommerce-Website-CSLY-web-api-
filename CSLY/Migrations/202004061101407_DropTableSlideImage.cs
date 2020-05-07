namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTableSlideImage : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.SlideImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SlideImages",
                c => new
                    {
                        SlideImageId = c.Int(nullable: false, identity: true),
                        SlideTitle = c.String(),
                        SlideImagePath = c.String(),
                    })
                .PrimaryKey(t => t.SlideImageId);
            
        }
    }
}
