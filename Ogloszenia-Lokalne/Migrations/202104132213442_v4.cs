namespace Ogloszenia_Lokalne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posters", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posters", "Views", c => c.String());
        }
    }
}
