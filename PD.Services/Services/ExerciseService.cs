using PD.Services.Contracts.Api.Exercises.Requests;
using PD.Services.Contracts.Api.Exercises.Responses;
using PD.Services.Contracts.Api.ExercisesDetails.Request;
using PD.Services.Contracts.Api.ExercisesDetails.Response;
using PD.Services.Contracts.Api.ExercisesEquipments.Requests;
using PD.Services.Contracts.Api.ExercisesEquipments.Responses;
using PowerlifterDiary.Models;
using System.Linq;
using System.Net;

namespace PD.Services.Services
{
    public class ExerciseService
    {
        public ServiceResponse<ExerciseResponse> Add(AddExerciseRequest exerciseRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var exerciseEquipment = db.ExerciseEquipment.FirstOrDefault(x => x.Id == exerciseRequest.ExerciseEquipmentId);
                if (exerciseEquipment == null)
                {
                    return new ServiceResponse<ExerciseResponse>(null, HttpStatusCode.NotFound, "Unable to find the equipment!");
                }
                var exercise = new Exercise
                {
                    Name = exerciseRequest.Name,
                    Description = exerciseRequest.Description,
                    ExerciseEquipment = exerciseEquipment

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

        public ServiceResponse<ExerciseDetailsResponse> Add(AddExerciseDetailsRequest exerciseRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var exerciseDetails = new ExerciseDetails
                {
                    Eccentric = exerciseRequest.Eccentric,
                    EccentricPause = exerciseRequest.EccentricPause,
                    Concetric = exerciseRequest.Concetric,
                    ConcetricPause = exerciseRequest.ConcetricPause,
                    Repeats = exerciseRequest.Repeats,
                    Series = exerciseRequest.Series
                };
                ExerciseDetails _exercise = db.ExercisesDetails.Add(exerciseDetails);
                db.SaveChanges();
                return new ServiceResponse<ExerciseDetailsResponse>(new ExerciseDetailsResponse(_exercise), HttpStatusCode.OK, "Exercise added succesfully!");
            }
        }

        //public ExerciseEquipmentResponse GetExerciseDetails(int id)
        //{
        //    using (DiaryContext db = new DiaryContext())
        //    {
        //        ExerciseDetails exercise = db.ExercisesDetails.Include("ExerciseTrainings").FirstOrDefault(x => x.Id == id);
        //        if (exercise == null)
        //            return null;
        //        return new ExerciseEquipmentResponse(exercise);
        //    }
        //}

        public ServiceResponse<ExerciseEquipmentResponse> Add(AddExerciseEquipmentRequest exerciseRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var exerciseEquipment = new ExerciseEquipment
                {
                    Name = exerciseRequest.Name
                };
                ExerciseEquipment _exercise = db.ExerciseEquipment.Add(exerciseEquipment);
                db.SaveChanges();
                return new ServiceResponse<ExerciseEquipmentResponse>(new ExerciseEquipmentResponse(_exercise), HttpStatusCode.OK, "Exercise added succesfully!");
            }
        }


    }
}
