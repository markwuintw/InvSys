namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbWebArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebArticles", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebArticles", "Content");
        }
    }
}
