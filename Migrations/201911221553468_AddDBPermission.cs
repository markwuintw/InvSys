namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDBPermission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Permission = c.Int(nullable: false),
                        Controller = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Permissions");
        }
    }
}
