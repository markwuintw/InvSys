namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creadeAreaAdminWebArticle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebArticles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Article = c.String(),
                        viewers = c.Int(nullable: false),
                        publishUser = c.String(),
                        publishDate = c.DateTime(),
                        UpdateUser = c.String(),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WebArticles");
        }
    }
}
