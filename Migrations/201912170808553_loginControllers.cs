namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loginControllers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime());
        }
    }
}
