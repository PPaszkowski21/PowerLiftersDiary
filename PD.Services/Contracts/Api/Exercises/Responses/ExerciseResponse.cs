using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Exercises.Responses
{
    public class ExerciseResponse
    {
        public ExerciseResponse(Exercise exercise)
        {
            Id = exercise.Id;
            Name = exercise.Name;
            Description = exercise.Description;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public ExerciseEquipmentResponse ExerciseEquipment { get; set; }
    }
}
