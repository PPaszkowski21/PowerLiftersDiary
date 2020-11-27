using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerlifterDiary.Models
{
    public class ExerciseTraining
    {
        public int Id { get; set; }
        public Exercise Exercise { get; set; }
        public ExerciseDetails ExerciseDetails { get; set; }
        public TrainingUnit TrainingUnit { get; set; }
    }
}