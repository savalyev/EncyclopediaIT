namespace EncyclopediaIT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Technologies", t => t.TechnologyId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TechnologyId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImagePath = c.String(),
                        ViewCount = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Rating = c.Int(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Technologies", t => t.TechnologyId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TechnologyId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "TechnologyId", "dbo.Technologies");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookmarks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Technologies", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Bookmarks", "TechnologyId", "dbo.Technologies");
            DropIndex("dbo.Comments", new[] { "TechnologyId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Technologies", new[] { "CategoryId" });
            DropIndex("dbo.Bookmarks", new[] { "TechnologyId" });
            DropIndex("dbo.Bookmarks", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Technologies");
            DropTable("dbo.Bookmarks");
        }
    }
}
