using PD.Services.Contracts.Api.ExerciseTrainings.Requests;
using PD.Services.Contracts.Api.ExerciseTrainings.Responses;
using PD.Services.Contracts.Api.TrainingUnits.Requests;
using PD.Services.Contracts.Api.TrainingUnits.Responses;
using PowerlifterDiary.Models;
using System.Linq;
using System.Net;

namespace PD.Services.Services
{
    public class TrainingService
    {
        public ServiceResponse<TrainingUnitResponse> Add(AddTrainingUnitRequest trainingUnitRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var day = db.Days.FirstOrDefault(x => x.Id == trainingUnitRequest.DayId);
                if (day == null)
                {
                    return new ServiceResponse<TrainingUnitResponse>(null, HttpStatusCode.NotFound, "Unable to find the day!");
                }

                var trainingUnit = new TrainingUnit
                {
                    Day = day
                };
                TrainingUnit _trainingUnit = db.TrainingUnits.Add(trainingUnit);
                db.SaveChanges();
                return new ServiceResponse<TrainingUnitResponse>(new TrainingUnitResponse(_trainingUnit), HttpStatusCode.OK, "Training unit added succesfully!");
            }
        }

        public ServiceResponse<ExerciseTrainingResponse> Add(AddExerciseTrainingRequest addExerciseTrainingRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var trainingUnit = db.TrainingUnits.FirstOrDefault(x => x.Id == addExerciseTrainingRequest.TrainingUnitId);
                if (trainingUnit == null)
                {
                    return new ServiceResponse<ExerciseTrainingResponse>(null, HttpStatusCode.NotFound, "Unable to find the training unit!");
                }

                var exercise = db.Exercises.FirstOrDefault(x => x.Id == addExerciseTrainingRequest.ExerciseId);
                if(exercise == null)
                {
                    return new ServiceResponse<ExerciseTrainingResponse>(null, HttpStatusCode.NotFound, "Unable to find the exercise!");
                }

                var exerciseDetails = db.ExercisesDetails.FirstOrDefault(x => x.Id == addExerciseTrainingRequest.ExerciseDetailsId);
                if(exerciseDetails == null)
                {
                    return new ServiceResponse<ExerciseTrainingResponse>(null, HttpStatusCode.NotFound, "Unable to find the exercise details!");
                }
                var exerciseTraining = new ExerciseTraining
                {
                    TrainingUnit = trainingUnit,
                    Exercise = exercise,
                    ExerciseDetails = exerciseDetails
                };
                ExerciseTraining _ExerciseTraining = db.ExerciseTrainings.Add(exerciseTraining);
                db.SaveChanges();
                return new ServiceResponse<ExerciseTrainingResponse>(new ExerciseTrainingResponse(_ExerciseTraining), HttpStatusCode.OK, "ExerciseTraining added succesfully!");
            }
        }

        public TrainingUnitResponse GetTrainingUnit(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                TrainingUnit trainingUnit = db.TrainingUnits.Include("Day").Include("ExerciseTrainings").FirstOrDefault(x => x.Id == id);
                if (trainingUnit == null)
                    return null;
                return new TrainingUnitResponse(trainingUnit);
            }
        }

        public ExerciseTrainingResponse GetExerciseTraining(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                ExerciseTraining exerciseTraining = db.ExerciseTrainings.Include("Exercise").Include("ExerciseDetails").Include("TrainingUnit").FirstOrDefault(x => x.Id == id);
                if (exerciseTraining == null)
                    return null;
                return new ExerciseTrainingResponse(exerciseTraining);
            }
        }

        public ServiceResponse DeleteExerciseTraining(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.ExerciseTrainings.Any(x => x.Id == id))
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "There is not existing exercise training with given id!");
                }

                var exerciseToRemove = db.ExerciseTrainings.Include("Exercise").Include("ExerciseDetails").Include("TrainingUnit").FirstOrDefault(x => x.Id == id);
                db.ExerciseTrainings.Remove(exerciseToRemove);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "Exercise Training deleted!");
        }

        public ServiceResponse DeleteTrainingUnit(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.TrainingUnits.Any(x => x.Id == id))
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "There is not existing training unit with given id!");
                }

                var trainingToRemove = db.TrainingUnits.Include("Day").Include("ExerciseTrainings").FirstOrDefault(x => x.Id == id);
                foreach (var exerciseTraining in trainingToRemove.ExerciseTrainings)
                {
                    DeleteExerciseTraining(exerciseTraining.Id);
                }
                db.SaveChanges();
            }
            using(DiaryContext db = new DiaryContext())
            {
                var trainingToRemove2 = db.TrainingUnits.Include("Day").Include("ExerciseTrainings").FirstOrDefault(x => x.Id == id);
                db.TrainingUnits.Remove(trainingToRemove2);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "Training unit deleted!");
        }

        //public ServiceResponse<TrainingUnitResponse> ReadById(int id)
        //{
        //    TrainingUnitResponse trainingUnitResponse = GetTrainingUnit(id);
        //    if (trainingUnitResponse == null)
        //    {
        //        return new ServiceResponse<TrainingUnitResponse>(null, HttpStatusCode.NotFound, "There is not existing training unit with given id!");
        //    }
        //    return new ServiceResponse<TrainingUnitResponse>(trainingUnitResponse, HttpStatusCode.OK, "Diary downloaded!");
        //}
    }
}
