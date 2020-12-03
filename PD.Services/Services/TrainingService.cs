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
                var exerciseTraining = new ExerciseTraining
                {
                    TrainingUnit = trainingUnit,
                    Exercise = exercise
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

        //public ServiceResponse Delete(int id)
        //{
        //    using (DiaryContext db = new DiaryContext())
        //    {
        //        if (!db.Days.Any(x => x.Id == id))
        //        {
        //            return new ServiceResponse(HttpStatusCode.NotFound, "There is not existing day with given id!");
        //        }

        //        var dayToRemove = db.Days.Include("Diary").Include("Dream").Include("TrainingUnits").FirstOrDefault(x => x.Id == id);
        //        //db.TrainingUnits.RemoveRange(dayToRemove.TrainingUnits);
        //        db.Dreams.Remove(dayToRemove.Dream);
        //        db.Days.Remove(dayToRemove);
        //        db.SaveChanges();
        //    }
        //    return new ServiceResponse(HttpStatusCode.OK, "Day deleted!");
        //}

        //public ServiceResponse<IEnumerable<IDay>> Read()
        //{
        //    List<Day> days = new List<Day>();
        //    using (DiaryContext db = new DiaryContext())
        //    {
        //        days = db.Days.Include("Diary").Include("Dream").Include("TrainingUnits").ToList();
        //    }
        //    List<DayResponse> dayResponses = new List<DayResponse>();
        //    foreach (var item in days)
        //    {
        //        dayResponses.Add(new DayResponse(item, typeof(DayResponse)));
        //    }
        //    return new ServiceResponse<IEnumerable<IDay>>(dayResponses, HttpStatusCode.OK, "Table downloaded!");
        //}

        public ServiceResponse<TrainingUnitResponse> ReadById(int id)
        {
            TrainingUnitResponse trainingUnitResponse = GetTrainingUnit(id);
            if (trainingUnitResponse == null)
            {
                return new ServiceResponse<TrainingUnitResponse>(null, HttpStatusCode.NotFound, "There is not existing training unit with given id!");
            }
            return new ServiceResponse<TrainingUnitResponse>(trainingUnitResponse, HttpStatusCode.OK, "Diary downloaded!");
        }
        //public ServiceResponse<IDay> Update(IDay updateDayRequest)
        //{
        //    Type myType = updateDayRequest.GetType();
        //    PropertyInfo property = myType.GetProperty("Id");
        //    int id = (int)property.GetValue(updateDayRequest);
        //    Day dayToUpdate;
        //    using (DiaryContext db = new DiaryContext())
        //    {
        //        if (!db.Days.Any(x => x.Id == id))
        //        {
        //            return new ServiceResponse<IDay>(null, HttpStatusCode.NotFound, "There is not existing day with given id!");
        //        }
        //        dayToUpdate = db.Days.FirstOrDefault(x => x.Id == id);
        //        if (updateDayRequest.Date.Year > 2019)
        //        {
        //            dayToUpdate.Date = updateDayRequest.Date;
        //        }
        //        db.SaveChanges();
        //        return new ServiceResponse<IDay>(new DayResponse(dayToUpdate, typeof(DayResponse)), HttpStatusCode.OK, "User was updated successfully");
        //    }
        //}

        //public ServiceResponse<IDream> AddDream(IDream dreamRequest)
        //{
        //    using (DiaryContext db = new DiaryContext())
        //    {
        //        var dreamVerification = db.Days.FirstOrDefault(x => x.Id == dreamRequest.Id);
        //        if (dreamVerification == null || dreamVerification.Dream != null)
        //        {
        //            return new ServiceResponse<IDream>(null, HttpStatusCode.BadRequest, "Day does not exist or it already has a dream");
        //        }
        //        var dream = new Dream
        //        {
        //            Id = dreamRequest.Id,
        //            Length = dreamRequest.Length,
        //            Quality = dreamRequest.Quality,
        //            Day = db.Days.FirstOrDefault(x => x.Id == dreamRequest.Id)
        //        };
        //        var _dream = db.Dreams.Add(dream);
        //        db.SaveChanges();
        //        return new ServiceResponse<IDream>(new DreamResponse(_dream, typeof(DreamResponse)), HttpStatusCode.OK, "Dream added succesfully!");
        //    }
        //}

        //public ServiceResponse<IDream> UpdateDream(IDream updateDreamRequest)
        //{
        //    using (DiaryContext db = new DiaryContext())
        //    {
        //        Dream dreamToUpdate = db.Dreams.FirstOrDefault(x => x.Id == updateDreamRequest.Id);
        //        if (dreamToUpdate == null)
        //        {
        //            return new ServiceResponse<IDream>(null, HttpStatusCode.NotFound, "There is not existing dream with given id!");
        //        }
        //        dreamToUpdate = db.Dreams.FirstOrDefault(x => x.Id == updateDreamRequest.Id);
        //        if (updateDreamRequest.Length > 0)
        //        {
        //            dreamToUpdate.Length = updateDreamRequest.Length;
        //        }
        //        if (!string.IsNullOrEmpty(updateDreamRequest.Quality))
        //        {
        //            dreamToUpdate.Quality = updateDreamRequest.Quality;
        //        }
        //        db.SaveChanges();
        //        return new ServiceResponse<IDream>(new DreamResponse(dreamToUpdate, typeof(DreamResponse)), HttpStatusCode.OK, "UserDetails added succesfully!");
        //    }
        //}
    }
}
