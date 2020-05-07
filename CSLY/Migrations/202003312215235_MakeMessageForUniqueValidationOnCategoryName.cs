namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeMessageForUniqueValidationOnCategoryName : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Categories", name: "IX_CategoryName", newName: "This Name is already exit");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Categories", name: "This Name is already exit", newName: "IX_CategoryName");
        }
    }
}
