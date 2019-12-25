namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelRename2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvLetters", "InvLetterStatus", c => c.Int(nullable: false));
            AddColumn("dbo.InvTables", "ImvNum", c => c.String(nullable: false));
            AddColumn("dbo.InvTables", "InvDate", c => c.DateTime());
            DropColumn("dbo.InvLetters", "InpLetterStatus");
            DropColumn("dbo.InvTables", "ImpNum");
            DropColumn("dbo.InvTables", "InpDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvTables", "InpDate", c => c.DateTime());
            AddColumn("dbo.InvTables", "ImpNum", c => c.String(nullable: false));
            AddColumn("dbo.InvLetters", "InpLetterStatus", c => c.Int(nullable: false));
            DropColumn("dbo.InvTables", "InvDate");
            DropColumn("dbo.InvTables", "ImvNum");
            DropColumn("dbo.InvLetters", "InvLetterStatus");
        }
    }
}
