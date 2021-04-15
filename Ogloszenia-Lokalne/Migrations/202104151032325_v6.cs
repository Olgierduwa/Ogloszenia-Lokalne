namespace Ogloszenia_Lokalne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "CategoryParentID");
            RenameColumn(table: "dbo.Categories", name: "CategoryParent_CategoryID", newName: "CategoryParentID");
            RenameIndex(table: "dbo.Categories", name: "IX_CategoryParent_CategoryID", newName: "IX_CategoryParentID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Categories", name: "IX_CategoryParentID", newName: "IX_CategoryParent_CategoryID");
            RenameColumn(table: "dbo.Categories", name: "CategoryParentID", newName: "CategoryParent_CategoryID");
            AddColumn("dbo.Categories", "CategoryParentID", c => c.Int());
        }
    }
}
