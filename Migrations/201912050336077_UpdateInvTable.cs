namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvTables", "DropTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.InvTables", "DropReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvTables", "DropReason");
            DropColumn("dbo.InvTables", "DropTime");
        }
    }
}
