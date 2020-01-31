namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbDownloadContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Downloads", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Downloads", "Content");
        }
    }
}
