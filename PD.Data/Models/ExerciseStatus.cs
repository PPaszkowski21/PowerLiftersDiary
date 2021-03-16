using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Data.Models
{
    public class ExerciseStatus
    {
        public string BodyPart { get; set; }
        public int ExercisesDone { get; set; }
        public int ExercisesToDo { get; set; }
    }
}
