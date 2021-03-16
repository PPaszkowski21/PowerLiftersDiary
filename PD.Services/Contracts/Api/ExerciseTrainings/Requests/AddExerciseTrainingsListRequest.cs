using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.ExerciseTrainings.Requests
{
    public class AddExerciseTrainingsListRequest
    {
        [Required]
        public List<AddExerciseTrainingRequest> ExerciseTrainings { get; set; }
    }
}
