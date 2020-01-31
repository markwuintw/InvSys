namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbContact : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContactUS", "publishUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactUS", "publishUser", c => c.String());
        }
    }
}
