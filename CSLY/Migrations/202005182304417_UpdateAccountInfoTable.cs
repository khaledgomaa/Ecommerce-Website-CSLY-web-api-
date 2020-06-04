namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAccountInfoTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountInfoes", "UserName", c => c.String());
            AddColumn("dbo.AccountInfoes", "Email", c => c.String(nullable: false));
            AddColumn("dbo.AccountInfoes", "PhoneNumber", c => c.Int(nullable: false));
            DropColumn("dbo.AccountInfoes", "ClientPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountInfoes", "ClientPassword", c => c.String());
            DropColumn("dbo.AccountInfoes", "PhoneNumber");
            DropColumn("dbo.AccountInfoes", "Email");
            DropColumn("dbo.AccountInfoes", "UserName");
        }
    }
}
