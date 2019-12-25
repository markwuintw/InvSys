namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbInvAccountMngrName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvAccounts", "MngrName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvAccounts", "MngrName");
        }
    }
}
