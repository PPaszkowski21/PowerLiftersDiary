namespace PD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDatesFromDream : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Dreams", "Start");
            DropColumn("dbo.Dreams", "End");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dreams", "End", c => c.DateTime(nullable: false));
            AddColumn("dbo.Dreams", "Start", c => c.DateTime(nullable: false));
        }
    }
}
