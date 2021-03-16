using PD.Services.Contracts.Api.Exercises.Requests;
using PD.Services.Contracts.Api.Exercises.Responses;
using PD.Services.Contracts.Api.ExercisesDetails.Request;
using PD.Services.Contracts.Api.ExercisesDetails.Response;
using PD.Services.Contracts.Api.ExercisesEquipments.Requests;
using PD.Services.Contracts.Api.ExercisesEquipments.Responses;
using PowerlifterDiary.Models;
using System.Collections.Generic;
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
                    ExerciseEquipment = exerciseEquipment,
                    BodyPart = exerciseRequest.BodyPart

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

        public ServiceResponse<ICollection<ExerciseEquipmentResponse>> GetAllExerciseEquipment()
        {
            using(DiaryContext db = new DiaryContext())
            {
                var list = db.ExerciseEquipment.ToList();
                List<ExerciseEquipmentResponse> list2 = new List<ExerciseEquipmentResponse>();
                foreach (var item in list)
                {
                    list2.Add(new ExerciseEquipmentResponse(item));
                }
                return new ServiceResponse<ICollection<ExerciseEquipmentResponse>>(list2,HttpStatusCode.OK,"Table downloaded successfully");
            }
        }
        public ServiceResponse<ICollection<ExerciseResponse>> GetAllExercises()
        {
            using (DiaryContext db = new DiaryContext())
            {
                var list = db.Exercises.ToList();
                List<ExerciseResponse> list2 = new List<ExerciseResponse>();
                foreach (var item in list)
                {
                    list2.Add(new ExerciseResponse(item));
                }
                return new ServiceResponse<ICollection<ExerciseResponse>>(list2, HttpStatusCode.OK, "Table downloaded successfully");
            }
        }
        public ServiceResponse<ICollection<ExerciseDetailsResponse>> GetAllExerciseDetails()
        {
            using (DiaryContext db = new DiaryContext())
            {
                var list = db.ExercisesDetails.ToList();
                List<ExerciseDetailsResponse> list2 = new List<ExerciseDetailsResponse>();
                foreach (var item in list)
                {
                    list2.Add(new ExerciseDetailsResponse(item));
                }
                return new ServiceResponse<ICollection<ExerciseDetailsResponse>>(list2, HttpStatusCode.OK, "Table downloaded successfully");
            }
        }
        public ExerciseEquipmentResponse GetExerciseEquipment(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                ExerciseEquipment exercise = db.ExerciseEquipment.FirstOrDefault(x => x.Id == id);
                if (exercise == null)
                    return null;
                return new ExerciseEquipmentResponse(exercise);
            }
        }

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

        public ServiceResponse DeleteExercise(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Exercises.Any(x => x.Id == id))
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "There is not existing exercise with given id!");
                }

                var exerciseToRemove = db.Exercises.Include("ExerciseEquipment").Include("ExerciseTrainings").FirstOrDefault(x => x.Id == id);
                TrainingService trainingService = new TrainingService();
                foreach (var exerciseTraining in exerciseToRemove.ExerciseTrainings)
                {
                    trainingService.DeleteExerciseTraining(exerciseTraining.Id);
                }
                db.Exercises.Remove(exerciseToRemove);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "Exercise deleted!");
        }
    }
}
