using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerlifterDiary.Models
{
    public class ExerciseEquipment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}