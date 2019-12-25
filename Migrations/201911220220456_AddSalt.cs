namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Salt", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Salt");
        }
    }
}
