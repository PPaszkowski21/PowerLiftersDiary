using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PowerlifterDiary.Models
{
    public class DiaryContext : DbContext
    {
        public DiaryContext():base("name=ConnectionString")
        {

        }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseEquipment> ExerciseEquipment { get; set; }
        public DbSet<ExerciseDetails> ExercisesDetails { get; set; }
        public DbSet<ExerciseTraining> ExerciseTrainings { get; set; }
        public DbSet<TrainingUnit> TrainingUnits { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Dream> Dreams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

    }
}