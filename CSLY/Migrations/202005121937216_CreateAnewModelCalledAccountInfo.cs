namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAnewModelCalledAccountInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountInfoes");
        }
    }
}
