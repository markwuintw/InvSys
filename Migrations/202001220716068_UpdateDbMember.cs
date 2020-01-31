namespace Sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "BirthDate", c => c.DateTime());
            AddColumn("dbo.Members", "ApplicationType", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "CellPhoneNumber", c => c.String());
            AddColumn("dbo.Members", "Address", c => c.String());
            AddColumn("dbo.Members", "InternationalMembership", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "InternationalMembershipFile", c => c.String());
            AddColumn("dbo.Members", "Company", c => c.String());
            AddColumn("dbo.Members", "HighEducation", c => c.String());
            AddColumn("dbo.Members", "ExpCom1", c => c.String());
            AddColumn("dbo.Members", "ExpJob1", c => c.String());
            AddColumn("dbo.Members", "ExpSDY1", c => c.Int());
            AddColumn("dbo.Members", "ExpSDM1", c => c.Int());
            AddColumn("dbo.Members", "ExpEDY1", c => c.Int());
            AddColumn("dbo.Members", "ExpEDM1", c => c.Int());
            AddColumn("dbo.Members", "ExpCom2", c => c.String());
            AddColumn("dbo.Members", "ExpJob2", c => c.String());
            AddColumn("dbo.Members", "ExpSDY2", c => c.Int());
            AddColumn("dbo.Members", "ExpSDM2", c => c.Int());
            AddColumn("dbo.Members", "ExpEDY2", c => c.Int());
            AddColumn("dbo.Members", "ExpEDM2", c => c.Int());
            AddColumn("dbo.Members", "ExpCom3", c => c.String());
            AddColumn("dbo.Members", "ExpJob3", c => c.String());
            AddColumn("dbo.Members", "ExpSDY3", c => c.Int());
            AddColumn("dbo.Members", "ExpSDM3", c => c.Int());
            AddColumn("dbo.Members", "ExpEDY3", c => c.Int());
            AddColumn("dbo.Members", "ExpEDM3", c => c.Int());
            AddColumn("dbo.Members", "TotalJobYear", c => c.Int());
            AddColumn("dbo.Members", "TotalJobMonth", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "TotalJobMonth");
            DropColumn("dbo.Members", "TotalJobYear");
            DropColumn("dbo.Members", "ExpEDM3");
            DropColumn("dbo.Members", "ExpEDY3");
            DropColumn("dbo.Members", "ExpSDM3");
            DropColumn("dbo.Members", "ExpSDY3");
            DropColumn("dbo.Members", "ExpJob3");
            DropColumn("dbo.Members", "ExpCom3");
            DropColumn("dbo.Members", "ExpEDM2");
            DropColumn("dbo.Members", "ExpEDY2");
            DropColumn("dbo.Members", "ExpSDM2");
            DropColumn("dbo.Members", "ExpSDY2");
            DropColumn("dbo.Members", "ExpJob2");
            DropColumn("dbo.Members", "ExpCom2");
            DropColumn("dbo.Members", "ExpEDM1");
            DropColumn("dbo.Members", "ExpEDY1");
            DropColumn("dbo.Members", "ExpSDM1");
            DropColumn("dbo.Members", "ExpSDY1");
            DropColumn("dbo.Members", "ExpJob1");
            DropColumn("dbo.Members", "ExpCom1");
            DropColumn("dbo.Members", "HighEducation");
            DropColumn("dbo.Members", "Company");
            DropColumn("dbo.Members", "InternationalMembershipFile");
            DropColumn("dbo.Members", "InternationalMembership");
            DropColumn("dbo.Members", "Address");
            DropColumn("dbo.Members", "CellPhoneNumber");
            DropColumn("dbo.Members", "ApplicationType");
            DropColumn("dbo.Members", "BirthDate");
        }
    }
}
