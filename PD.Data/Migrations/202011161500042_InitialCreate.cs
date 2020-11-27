namespace PD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Diary_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diaries", t => t.Diary_Id)
                .Index(t => t.Diary_Id);
            
            CreateTable(
                "dbo.Diaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Conclusions = c.String(),
                        BenchPressStart = c.Int(nullable: false),
                        SquatStart = c.Int(nullable: false),
                        DeadliftStart = c.Int(nullable: false),
                        BenchPressEnd = c.Int(nullable: false),
                        SquatEnd = c.Int(nullable: false),
                        DeadliftEnd = c.Int(nullable: false),
                        Progress = c.Single(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                        Age = c.Int(nullable: false),
                        BMR = c.Single(nullable: false),
                        BMI = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Dreams",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Length = c.Single(nullable: false),
                        Quality = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Days", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.TrainingUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Days", t => t.Day_Id)
                .Index(t => t.Day_Id);
            
            CreateTable(
                "dbo.ExerciseTrainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Exercise_Id = c.Int(),
                        ExerciseDetails_Id = c.Int(),
                        TrainingUnit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Exercise_Id)
                .ForeignKey("dbo.ExerciseDetails", t => t.ExerciseDetails_Id)
                .ForeignKey("dbo.TrainingUnits", t => t.TrainingUnit_Id)
                .Index(t => t.Exercise_Id)
                .Index(t => t.ExerciseDetails_Id)
                .Index(t => t.TrainingUnit_Id);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ExerciseEquipment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExerciseEquipments", t => t.ExerciseEquipment_Id)
                .Index(t => t.ExerciseEquipment_Id);
            
            CreateTable(
                "dbo.ExerciseEquipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExerciseDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Eccentric = c.Single(nullable: false),
                        EccentricPause = c.Int(nullable: false),
                        Concetric = c.Single(nullable: false),
                        ConcetricPause = c.Int(nullable: false),
                        Series = c.Int(nullable: false),
                        Repeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseTrainings", "TrainingUnit_Id", "dbo.TrainingUnits");
            DropForeignKey("dbo.ExerciseTrainings", "ExerciseDetails_Id", "dbo.ExerciseDetails");
            DropForeignKey("dbo.ExerciseTrainings", "Exercise_Id", "dbo.Exercises");
            DropForeignKey("dbo.Exercises", "ExerciseEquipment_Id", "dbo.ExerciseEquipments");
            DropForeignKey("dbo.TrainingUnits", "Day_Id", "dbo.Days");
            DropForeignKey("dbo.Dreams", "Id", "dbo.Days");
            DropForeignKey("dbo.UserDetails", "Id", "dbo.Users");
            DropForeignKey("dbo.Diaries", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Days", "Diary_Id", "dbo.Diaries");
            DropIndex("dbo.Exercises", new[] { "ExerciseEquipment_Id" });
            DropIndex("dbo.ExerciseTrainings", new[] { "TrainingUnit_Id" });
            DropIndex("dbo.ExerciseTrainings", new[] { "ExerciseDetails_Id" });
            DropIndex("dbo.ExerciseTrainings", new[] { "Exercise_Id" });
            DropIndex("dbo.TrainingUnits", new[] { "Day_Id" });
            DropIndex("dbo.Dreams", new[] { "Id" });
            DropIndex("dbo.UserDetails", new[] { "Id" });
            DropIndex("dbo.Diaries", new[] { "User_Id" });
            DropIndex("dbo.Days", new[] { "Diary_Id" });
            DropTable("dbo.ExerciseDetails");
            DropTable("dbo.ExerciseEquipments");
            DropTable("dbo.Exercises");
            DropTable("dbo.ExerciseTrainings");
            DropTable("dbo.TrainingUnits");
            DropTable("dbo.Dreams");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Users");
            DropTable("dbo.Diaries");
            DropTable("dbo.Days");
        }
    }
}
