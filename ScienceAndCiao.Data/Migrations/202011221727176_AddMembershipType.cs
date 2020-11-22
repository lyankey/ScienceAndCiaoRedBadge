namespace ScienceAndCiao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        SignUpFee = c.Byte(nullable: false),
                        MonthlyMembershipFee = c.Byte(nullable: false),
                        SixMonthMemberShipFee = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MembershipType");
        }
    }
}
