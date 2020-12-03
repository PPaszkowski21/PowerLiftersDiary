using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.ExercisesDetails.Request
{
    public class AddExerciseDetailsRequest
    {
        [Required]
        public float Eccentric { get; set; }
        [Required]
        public int EccentricPause { get; set; }
        [Required]
        public float Concetric { get; set; }
        [Required]
        public int ConcetricPause { get; set; }
        [Required]
        public int Series { get; set; }
        [Required]
        public int Repeats { get; set; }
    }
}
