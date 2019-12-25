namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelInitDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime());
            AlterColumn("dbo.WebArticles", "InitDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WebArticles", "InitDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime(nullable: false));
        }
    }
}
