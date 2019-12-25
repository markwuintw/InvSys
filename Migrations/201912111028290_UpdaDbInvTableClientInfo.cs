namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdaDbInvTableClientInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvTables", "ClientUniformNum", c => c.String(nullable: false));
            AddColumn("dbo.InvTables", "ClientAdress", c => c.String(nullable: false));
            AddColumn("dbo.InvTables", "ClientPhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvTables", "ClientPhoneNumber");
            DropColumn("dbo.InvTables", "ClientAdress");
            DropColumn("dbo.InvTables", "ClientUniformNum");
        }
    }
}
