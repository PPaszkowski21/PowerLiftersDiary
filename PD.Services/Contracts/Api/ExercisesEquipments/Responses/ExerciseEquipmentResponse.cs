using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.ExercisesEquipments.Responses
{
    public class ExerciseEquipmentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ExerciseEquipmentResponse(ExerciseEquipment exercise)
        {
            Id = exercise.Id;
            Name = exercise.Name;
        }
    }
}
