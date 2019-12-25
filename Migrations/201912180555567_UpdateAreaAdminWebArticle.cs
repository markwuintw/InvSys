namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAreaAdminWebArticle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WebArticles", "Article", c => c.String(nullable: false));
            AlterColumn("dbo.WebArticles", "publishUser", c => c.String(nullable: false));
            AlterColumn("dbo.WebArticles", "publishDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WebArticles", "UpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.WebArticles", "InitDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WebArticles", "InitDate", c => c.DateTime());
            AlterColumn("dbo.WebArticles", "UpdateUser", c => c.String());
            AlterColumn("dbo.WebArticles", "publishDate", c => c.DateTime());
            AlterColumn("dbo.WebArticles", "publishUser", c => c.String());
            AlterColumn("dbo.WebArticles", "Article", c => c.String());
        }
    }
}
