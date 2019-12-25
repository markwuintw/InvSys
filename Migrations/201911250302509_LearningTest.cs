namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LearningTest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime());
        }
    }
}
