using System.Collections.Generic;

namespace PowerlifterDiary.Models
{
    public class TrainingUnit
    {
        public int Id { get; set; }
        public virtual Day Day { get; set; }
        public virtual ICollection<ExerciseTraining> ExerciseTrainings { get; set; }
    }
}