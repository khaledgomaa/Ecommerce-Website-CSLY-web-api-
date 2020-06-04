namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAccountInfoTableDeleteUserName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AccountInfoes", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccountInfoes", "UserName", c => c.String());
        }
    }
}
