using System.Collections.Generic;

namespace PowerlifterDiary.Models
{
    public class TrainingUnit
    {
        public int Id { get; set; }
        public Day Day { get; set; }
        public ICollection<ExerciseTraining> ExerciseTrainings { get; set; }
    }
}