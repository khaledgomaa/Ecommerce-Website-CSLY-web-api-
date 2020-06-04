namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateShippingForiegnKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Shippings", name: "ClientId_Id", newName: "ClientId");
            RenameIndex(table: "dbo.Shippings", name: "IX_ClientId_Id", newName: "IX_ClientId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Shippings", name: "IX_ClientId", newName: "IX_ClientId_Id");
            RenameColumn(table: "dbo.Shippings", name: "ClientId", newName: "ClientId_Id");
        }
    }
}
