namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbAllBooleanType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AboutLinks", "OnSite", c => c.Int(nullable: false));
            AlterColumn("dbo.AboutLinks", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Banners", "OnSite", c => c.Int(nullable: false));
            AlterColumn("dbo.Banners", "InitDate", c => c.DateTime());
            AlterColumn("dbo.ContactUS", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Downloads", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Experts", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Knowledges", "Top", c => c.Int(nullable: false));
            AlterColumn("dbo.Knowledges", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime());
            AlterColumn("dbo.News", "Top", c => c.Int(nullable: false));
            AlterColumn("dbo.News", "HighLight", c => c.Int(nullable: false));
            AlterColumn("dbo.News", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Permissions", "InitDate", c => c.DateTime());
            AlterColumn("dbo.WebArticles", "InitDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WebArticles", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Permissions", "InitDate", c => c.DateTime());
            AlterColumn("dbo.News", "InitDate", c => c.DateTime());
            AlterColumn("dbo.News", "HighLight", c => c.Boolean(nullable: false));
            AlterColumn("dbo.News", "Top", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Knowledges", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Knowledges", "Top", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Experts", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Downloads", "InitDate", c => c.DateTime());
            AlterColumn("dbo.ContactUS", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Banners", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Banners", "OnSite", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AboutLinks", "InitDate", c => c.DateTime());
            AlterColumn("dbo.AboutLinks", "OnSite", c => c.Boolean(nullable: false));
        }
    }
}
