using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.ExercisesEquipments.Requests
{
    public class AddExerciseEquipmentRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
