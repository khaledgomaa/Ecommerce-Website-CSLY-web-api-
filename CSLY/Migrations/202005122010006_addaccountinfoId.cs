namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addaccountinfoId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "AccountInfo_Id", "dbo.AccountInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "AccountInfo_Id" });
            AddColumn("dbo.AspNetUsers", "AccountInfo_Id1", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "AccountInfo_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "AccountInfo_Id1");
            AddForeignKey("dbo.AspNetUsers", "AccountInfo_Id1", "dbo.AccountInfoes", "Id");
        }
        
        public override void Down()
        {

            DropForeignKey("dbo.AspNetUsers", "AccountInfo_Id1", "dbo.AccountInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "AccountInfo_Id1" });
            AlterColumn("dbo.AspNetUsers", "AccountInfo_Id", c => c.Int());
            DropColumn("dbo.AspNetUsers", "AccountInfo_Id1");
            CreateIndex("dbo.AspNetUsers", "AccountInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "AccountInfo_Id", "dbo.AccountInfoes", "Id");
        }
    }
}
