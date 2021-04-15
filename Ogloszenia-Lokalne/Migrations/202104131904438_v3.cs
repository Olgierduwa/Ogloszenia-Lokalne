namespace Ogloszenia_Lokalne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPosters",
                c => new
                    {
                        UserPosterID = c.Int(nullable: false, identity: true),
                        ApplicationUserID = c.String(maxLength: 128),
                        PosterID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserPosterID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .ForeignKey("dbo.Posters", t => t.PosterID, cascadeDelete: true)
                .Index(t => t.ApplicationUserID)
                .Index(t => t.PosterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPosters", "PosterID", "dbo.Posters");
            DropForeignKey("dbo.UserPosters", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.UserPosters", new[] { "PosterID" });
            DropIndex("dbo.UserPosters", new[] { "ApplicationUserID" });
            DropTable("dbo.UserPosters");
        }
    }
}
