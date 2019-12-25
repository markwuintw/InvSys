namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaltNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "Salt", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "Salt", c => c.String(nullable: false));
        }
    }
}
