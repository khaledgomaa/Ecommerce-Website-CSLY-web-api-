namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dontcareofit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "AccountInfo_Id1", "dbo.AccountInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "AccountInfo_Id1" });
            DropColumn("dbo.AspNetUsers", "AccountInfo_Id");
            DropColumn("dbo.AspNetUsers", "AccountInfo_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AccountInfo_Id1", c => c.Int());
            AddColumn("dbo.AspNetUsers", "AccountInfo_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "AccountInfo_Id1");
            AddForeignKey("dbo.AspNetUsers", "AccountInfo_Id1", "dbo.AccountInfoes", "Id");
        }
    }
}
