namespace ScienceAndCiao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedNullableSymbols : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUser", "MembershipTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUser", "MembershipTypeId", c => c.Int());
        }
    }
}
