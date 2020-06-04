namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFieldtoAspNetusersTableCalledClientIdasForignKeyToAccountInfoModel : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.AspNetUsers", "AccountInfo_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "AccountInfo_Id");
            AddForeignKey("dbo.AspNetUsers", "AccountInfo_Id", "dbo.AccountInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "AccountInfo_Id", "dbo.AccountInfoes");
            DropIndex("dbo.AspNetUsers", new[] { "AccountInfo_Id" });
            DropColumn("dbo.AspNetUsers", "AccountInfo_Id");

        }
    }
}
