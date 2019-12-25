namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDBRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "CategoryId", "dbo.Members");
            DropIndex("dbo.Roles", new[] { "CategoryId" });
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        InitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Roles", "CategoryId");
            AddForeignKey("dbo.Roles", "CategoryId", "dbo.Members", "Id", cascadeDelete: true);
        }
    }
}
