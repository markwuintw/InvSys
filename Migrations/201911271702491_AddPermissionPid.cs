namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermissionPid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "PId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permissions", "PId");
        }
    }
}
