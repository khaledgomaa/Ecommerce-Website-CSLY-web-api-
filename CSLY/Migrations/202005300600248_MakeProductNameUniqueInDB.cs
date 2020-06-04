namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeProductNameUniqueInDB : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Products", "ProductName", unique: true, name: "This Name is already exit");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", "This Name is already exit");
        }
    }
}
