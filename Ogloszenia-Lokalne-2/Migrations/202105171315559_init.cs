namespace Ogloszenia_Lokalne_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        AdID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        IsValuable = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                        Content = c.String(nullable: false),
                        References = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        AddingDate = c.DateTime(nullable: false),
                        Views = c.Int(nullable: false),
                        OwnerID = c.String(maxLength: 128),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.AdID)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "dbo.AdCategories",
                c => new
                    {
                        AdCategoryID = c.Int(nullable: false, identity: true),
                        AdID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdCategoryID)
                .ForeignKey("dbo.Ads", t => t.AdID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.AdID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CategoryParentID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Categories", t => t.CategoryParentID)
                .Index(t => t.CategoryParentID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AdUsers",
                c => new
                    {
                        AdUserID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        AdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdUserID)
                .ForeignKey("dbo.Ads", t => t.AdID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.AdID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ads", "OwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AdUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AdUsers", "AdID", "dbo.Ads");
            DropForeignKey("dbo.Categories", "CategoryParentID", "dbo.Categories");
            DropForeignKey("dbo.AdCategories", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.AdCategories", "AdID", "dbo.Ads");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AdUsers", new[] { "AdID" });
            DropIndex("dbo.AdUsers", new[] { "UserID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Categories", new[] { "CategoryParentID" });
            DropIndex("dbo.AdCategories", new[] { "CategoryID" });
            DropIndex("dbo.AdCategories", new[] { "AdID" });
            DropIndex("dbo.Ads", new[] { "OwnerID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AdUsers");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Categories");
            DropTable("dbo.AdCategories");
            DropTable("dbo.Ads");
        }
    }
}
