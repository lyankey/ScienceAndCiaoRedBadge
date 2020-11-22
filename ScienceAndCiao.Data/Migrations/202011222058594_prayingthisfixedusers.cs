namespace ScienceAndCiao.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prayingthisfixedusers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUser", "Disable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUser", "Disable", c => c.Int(nullable: false));
        }
    }
}
