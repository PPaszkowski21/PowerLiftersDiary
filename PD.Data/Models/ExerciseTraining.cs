using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerlifterDiary.Models
{
    public class ExerciseTraining
    {
        public int Id { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual ExerciseDetails ExerciseDetails { get; set; }
        public virtual TrainingUnit TrainingUnit { get; set; }
    }
}