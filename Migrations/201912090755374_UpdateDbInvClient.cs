namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbInvClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvClientInfoes", "ContactName", c => c.String());
            AddColumn("dbo.InvClientInfoes", "ContactPhone", c => c.String());
            AddColumn("dbo.InvClientInfoes", "ContactEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvClientInfoes", "ContactEmail");
            DropColumn("dbo.InvClientInfoes", "ContactPhone");
            DropColumn("dbo.InvClientInfoes", "ContactName");
        }
    }
}
