namespace CSLY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteSelectListPropFromProduct : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Categories_DataGroupField");
            DropColumn("dbo.Products", "Categories_DataTextField");
            DropColumn("dbo.Products", "Categories_DataValueField");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Categories_DataValueField", c => c.String());
            AddColumn("dbo.Products", "Categories_DataTextField", c => c.String());
            AddColumn("dbo.Products", "Categories_DataGroupField", c => c.String());
        }
    }
}
