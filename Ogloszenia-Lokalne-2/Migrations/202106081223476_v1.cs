namespace Ogloszenia_Lokalne_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportID = c.Int(nullable: false, identity: true),
                        AdID = c.Int(nullable: false),
                        UserReportedID = c.String(),
                        ReportMessage = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReportID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reports");
        }
    }
}
