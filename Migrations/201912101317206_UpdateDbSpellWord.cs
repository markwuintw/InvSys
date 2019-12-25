namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbSpellWord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvTables", "InvNum", c => c.String(nullable: false));
            AddColumn("dbo.InvTables", "InvStatus", c => c.Int(nullable: false));
            DropColumn("dbo.InvTables", "ImvNum");
            DropColumn("dbo.InvTables", "InpStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvTables", "InpStatus", c => c.Int(nullable: false));
            AddColumn("dbo.InvTables", "ImvNum", c => c.String(nullable: false));
            DropColumn("dbo.InvTables", "InvStatus");
            DropColumn("dbo.InvTables", "InvNum");
        }
    }
}
