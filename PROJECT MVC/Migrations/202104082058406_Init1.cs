namespace PROJECT_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CategoryParentID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CategoryParentID", c => c.Int(nullable: true));
        }
    }
}
