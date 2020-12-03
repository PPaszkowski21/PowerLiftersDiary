using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Exercises.Requests
{
    public class AddExerciseRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        //public ExerciseEquipment ExerciseEquipment { get; set; }
    }
}
