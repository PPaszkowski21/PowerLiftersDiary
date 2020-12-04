using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerlifterDiary.Models
{
    public class ExerciseDetails
    {
        public int Id { get; set; }
        public float Eccentric { get; set; }
        public int EccentricPause { get; set; }
        public float Concetric { get; set; }
        public int ConcetricPause { get; set; }
        public int Series { get; set; }
        public int Repeats { get; set; }
        public virtual ICollection<ExerciseTraining> ExerciseTrainings { get; set; }

    }
}