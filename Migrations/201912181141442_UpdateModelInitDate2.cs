namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelInitDate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvAccounts", "InitDate", c => c.DateTime());
            AlterColumn("dbo.InvClientInfoes", "InitDate", c => c.DateTime());
            AlterColumn("dbo.InvLetters", "InitDate", c => c.DateTime());
            AlterColumn("dbo.InvTables", "InitDate", c => c.DateTime());
            AlterColumn("dbo.InvItems", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Permissions", "InitDate", c => c.DateTime());
            AlterColumn("dbo.WebArticles", "InitDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WebArticles", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Permissions", "InitDate", c => c.DateTime());
            AlterColumn("dbo.Members", "InitDate", c => c.DateTime());
            AlterColumn("dbo.InvItems", "InitDate", c => c.DateTime());
            AlterColumn("dbo.InvTables", "InitDate", c => c.DateTime());
            AlterColumn("dbo.InvLetters", "InitDate", c => c.DateTime());
            AlterColumn("dbo.InvClientInfoes", "InitDate", c => c.DateTime());
            AlterColumn("dbo.InvAccounts", "InitDate", c => c.DateTime());
        }
    }
}
