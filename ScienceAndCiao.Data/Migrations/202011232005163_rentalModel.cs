namespace ScienceAndCiao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rentalModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rental", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rental", "Status");
        }
    }
}
