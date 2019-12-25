namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Permission", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Permission");
        }
    }
}
