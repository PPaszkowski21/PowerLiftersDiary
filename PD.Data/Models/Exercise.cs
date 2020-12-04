using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerlifterDiary.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ExerciseEquipment ExerciseEquipment { get; set; }
        public virtual ICollection<ExerciseTraining> ExerciseTrainings { get; set; }
    }
}