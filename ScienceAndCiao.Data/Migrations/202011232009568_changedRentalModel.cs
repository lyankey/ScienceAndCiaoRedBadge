namespace ScienceAndCiao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedRentalModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rental", "Duration", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rental", "Duration");
        }
    }
}
