using PD.Services.Contracts.Api.Exercises.Requests;
using PD.Services.Contracts.Api.Exercises.Responses;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Services
{
    public class ExerciseService
    {
        public ServiceResponse<ExerciseResponse> Add(AddExerciseRequest exerciseRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var exercise = new Exercise
                {
                    Name = exerciseRequest.Name,
                    Description = exerciseRequest.Description
                };
                Exercise _exercise = db.Exercises.Add(exercise);
                db.SaveChanges();
                return new ServiceResponse<ExerciseResponse>(new ExerciseResponse(_exercise), HttpStatusCode.OK, "Exercise added succesfully!");
            }
        }

        public ExerciseResponse GetExercise(int id)
        {
            using(DiaryContext db = new DiaryContext())
            {
                Exercise exercise = db.Exercises.Include("ExerciseTrainings").Include("ExerciseEquipment").FirstOrDefault(x => x.Id == id);
                if (exercise == null)
                    return null;
                return new ExerciseResponse(exercise);
            }
        }
    }
}
