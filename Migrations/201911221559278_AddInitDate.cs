namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInitDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "InitDate", c => c.DateTime());
            AddColumn("dbo.Permissions", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Permissions", "Permission", c => c.String());
            AlterColumn("dbo.Permissions", "Controller", c => c.String());
            AlterColumn("dbo.Permissions", "Action", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Permissions", "Action", c => c.Int(nullable: false));
            AlterColumn("dbo.Permissions", "Controller", c => c.Int(nullable: false));
            AlterColumn("dbo.Permissions", "Permission", c => c.Int(nullable: false));
            DropColumn("dbo.Permissions", "InitDate");
            DropColumn("dbo.Members", "InitDate");
        }
    }
}
