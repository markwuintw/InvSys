namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvTables", "DropTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvTables", "DropTime", c => c.DateTime(nullable: false));
        }
    }
}
