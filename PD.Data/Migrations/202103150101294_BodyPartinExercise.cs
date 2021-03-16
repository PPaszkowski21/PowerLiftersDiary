namespace PD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BodyPartinExercise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "BodyPart", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "BodyPart");
        }
    }
}
