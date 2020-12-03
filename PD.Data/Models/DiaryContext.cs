using Microsoft.AspNet.Identity.EntityFramework;
using PD.Data.Models;
using System.Data.Entity;

namespace PowerlifterDiary.Models
{
    public class DiaryContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public DiaryContext() : base("name=RemoteConnectionString")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseEquipment> ExerciseEquipment { get; set; }
        public DbSet<ExerciseDetails> ExercisesDetails { get; set; }
        public DbSet<ExerciseTraining> ExerciseTrainings { get; set; }
        public DbSet<TrainingUnit> TrainingUnits { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<Dream> Dreams { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
    }
}