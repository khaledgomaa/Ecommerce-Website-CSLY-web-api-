namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecoverMyMistakeBuddy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SlideImages", "SlideImagePath", c => c.String());
            AddColumn("dbo.Roles", "RoleName", c => c.String());
            
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "RoleName");
            DropColumn("dbo.SlideImages", "SlideImagePath");
        }
    }
}
