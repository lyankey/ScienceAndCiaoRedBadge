namespace ScienceAndCiao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeMembershipTypeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUser", "MembershipTypeId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUser", "MembershipTypeId", c => c.Int(nullable: false));
        }
    }
}
