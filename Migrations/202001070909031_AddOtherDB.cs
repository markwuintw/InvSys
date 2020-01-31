namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item = c.String(nullable: false),
                        Photo = c.String(nullable: false),
                        LinkURL = c.String(nullable: false),
                        OnSite = c.Boolean(nullable: false),
                        order = c.Int(nullable: false),
                        publishDate = c.DateTime(),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item = c.String(nullable: false),
                        Photo = c.String(nullable: false),
                        LinkURL = c.String(nullable: false),
                        OnSite = c.Boolean(nullable: false),
                        order = c.Int(nullable: false),
                        publishUser = c.String(nullable: false),
                        publishDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactUS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item = c.String(nullable: false),
                        Content = c.String(),
                        Name = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        Tel = c.String(nullable: false),
                        publishUser = c.String(),
                        publishDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Downloads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item = c.String(nullable: false),
                        viewers = c.Int(nullable: false),
                        publishUser = c.String(nullable: false),
                        publishDate = c.DateTime(),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Experts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Job = c.String(nullable: false),
                        Photo = c.String(),
                        Education = c.String(nullable: false),
                        BasicIntroduction = c.String(),
                        RelativeIntroduction = c.String(),
                        publishUser = c.String(nullable: false),
                        publishDate = c.DateTime(),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Knowledges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item = c.String(nullable: false),
                        Introduction = c.String(nullable: false),
                        Photo = c.String(),
                        Top = c.Boolean(nullable: false),
                        Content = c.String(),
                        viewers = c.Int(nullable: false),
                        publishDate = c.DateTime(),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NewsItem = c.String(nullable: false),
                        Introduction = c.String(nullable: false),
                        Photo = c.String(nullable: false),
                        Top = c.Boolean(nullable: false),
                        HighLight = c.Boolean(nullable: false),
                        Content = c.String(),
                        viewers = c.Int(nullable: false),
                        publishDate = c.DateTime(),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
            DropTable("dbo.Knowledges");
            DropTable("dbo.Experts");
            DropTable("dbo.Downloads");
            DropTable("dbo.ContactUS");
            DropTable("dbo.Banners");
            DropTable("dbo.AboutLinks");
        }
    }
}
