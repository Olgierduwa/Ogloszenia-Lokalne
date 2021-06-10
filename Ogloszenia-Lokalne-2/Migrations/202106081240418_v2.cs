namespace Ogloszenia_Lokalne_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reports", "UserReportedID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reports", "UserReportedID");
            AddForeignKey("dbo.Reports", "UserReportedID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "UserReportedID", "dbo.AspNetUsers");
            DropIndex("dbo.Reports", new[] { "UserReportedID" });
            AlterColumn("dbo.Reports", "UserReportedID", c => c.String());
        }
    }
}
