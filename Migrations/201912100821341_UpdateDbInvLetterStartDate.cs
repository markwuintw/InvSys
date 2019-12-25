namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbInvLetterStartDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvLetters", "StartDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvLetters", "StartDate");
        }
    }
}
