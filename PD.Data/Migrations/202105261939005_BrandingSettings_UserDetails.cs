namespace PD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BrandingSettings_UserDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "BrandingSettings", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDetails", "BrandingSettings");
        }
    }
}
