namespace ScienceAndCiao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUser", "FirstName", c => c.String());
            AddColumn("dbo.ApplicationUser", "LastName", c => c.String());
            AddColumn("dbo.ApplicationUser", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApplicationUser", "Disable", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUser", "MemberShipTypeId", c => c.String());
            AddColumn("dbo.ApplicationUser", "IsSubscribedToEmails", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUser", "IsSubscribedToEmails");
            DropColumn("dbo.ApplicationUser", "MemberShipTypeId");
            DropColumn("dbo.ApplicationUser", "Disable");
            DropColumn("dbo.ApplicationUser", "BirthDate");
            DropColumn("dbo.ApplicationUser", "LastName");
            DropColumn("dbo.ApplicationUser", "FirstName");
        }
    }
}
