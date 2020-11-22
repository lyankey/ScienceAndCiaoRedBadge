namespace ScienceAndCiao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMembershipTypeDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[MembershipType](Name,SignUpFee,MonthlyMembershipFee,SixMonthMemberShipFee) VALUES('Pay Per Rental', 0,50,25)");
            Sql("INSERT INTO [dbo].[MembershipType](Name,SignUpFee,MonthlyMembershipFee,SixMonthMemberShipFee) VALUES('Member', 10,20,10)");
            Sql("INSERT INTO [dbo].[MembershipType](Name,SignUpFee,MonthlyMembershipFee,SixMonthMemberShipFee) VALUES('SAdmin', 0,0,0)");
        }
        
        public override void Down()
        {
        }
    }
}
