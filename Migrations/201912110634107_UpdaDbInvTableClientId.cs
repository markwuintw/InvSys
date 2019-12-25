namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdaDbInvTableClientId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvTables", "ClientId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvTables", "ClientId");
        }
    }
}
