using PD.Services.Contracts.Api.Days.Requests;
using PD.Services.Contracts.Api.Days.Responses;
using PD.Services.Contracts.Api.Dreams.Requests;
using PD.Services.Contracts.Api.Dreams.Responses;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace PD.Services.Services
{
    public class DayService
    {
        public ServiceResponse<DayResponse> Add(AddDayRequest dayRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var diary = db.Diaries.FirstOrDefault(x => x.Id == dayRequest.DiaryId);
                if (diary == null)
                {
                    return new ServiceResponse<DayResponse>(null, HttpStatusCode.NotFound, "Unable to find the diary!");
                }

                var day = new Day
                {
                    Date = dayRequest.Date,
                    Diary = diary,
                };
                Day _day = db.Days.Add(day);
                db.SaveChanges();
                return new ServiceResponse<DayResponse>(new DayResponse(_day,typeof(DayResponse)), HttpStatusCode.OK, "Day added succesfully!");
            }
        }

        public DayResponse GetDay(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                Day day = db.Days.Include("Diary").Include("Dream").Include("TrainingUnits").FirstOrDefault(x => x.Id == id);
                if (day == null)
                    return null;
                return new DayResponse(day, typeof(DayResponse));
            }
        }

        public ServiceResponse Delete(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Days.Any(x => x.Id == id))
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "There is not existing day with given id!");
                }

                var dayToRemove = db.Days.Include("Diary").Include("Dream").Include("TrainingUnits").FirstOrDefault(x => x.Id == id);
                TrainingService trainingService = new TrainingService();
                foreach (var trainingUnit in dayToRemove.TrainingUnits)
                {
                    trainingService.DeleteTrainingUnit(trainingUnit.Id);
                }
                if(dayToRemove.Dream != null)
                {
                    db.Dreams.Remove(dayToRemove.Dream);
                }
                db.Days.Remove(dayToRemove);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "Day deleted!");
        }

        public ServiceResponse<IEnumerable<DayResponse>> Read()
        {
            List<Day> days = new List<Day>();
            using (DiaryContext db = new DiaryContext())
            {
                days = db.Days.Include("Diary").Include("Dream").Include("TrainingUnits").ToList();
            }
            List<DayResponse> dayResponses = new List<DayResponse>();
            foreach (var item in days)
            {
                dayResponses.Add(new DayResponse(item,typeof(DayResponse)));
            }
            return new ServiceResponse<IEnumerable<DayResponse>>(dayResponses, HttpStatusCode.OK, "Table downloaded!");
        }

        public ServiceResponse<DayResponse> ReadById(int id)
        {
            DayResponse dayResponse = GetDay(id);
            if (dayResponse == null)
            {
                return new ServiceResponse<DayResponse>(null, HttpStatusCode.NotFound, "There is not existing day with given id!");
            }
            return new ServiceResponse<DayResponse>(dayResponse, HttpStatusCode.OK, "Diary downloaded!");
        }
        public ServiceResponse<DayResponse> Update(UpdateDayRequest updateDayRequest)
        {
            Day dayToUpdate;
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Days.Any(x => x.Id == updateDayRequest.Id))
                {
                    return new ServiceResponse<DayResponse>(null, HttpStatusCode.NotFound, "There is not existing day with given id!");
                }
                dayToUpdate = db.Days.FirstOrDefault(x => x.Id == updateDayRequest.Id);
                if (updateDayRequest.Date.Year > 2019)
                {
                    dayToUpdate.Date = updateDayRequest.Date;
                }
                db.SaveChanges();
                return new ServiceResponse<DayResponse>(new DayResponse(dayToUpdate,typeof(DayResponse)), HttpStatusCode.OK, "User was updated successfully");
            }
        }

        public ServiceResponse<DreamResponse> AddDream(AddDreamRequest dreamRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var dreamVerification = db.Days.FirstOrDefault(x => x.Id == dreamRequest.Id);
                if (dreamVerification == null || dreamVerification.Dream != null)
                {
                    return new ServiceResponse<DreamResponse>(null, HttpStatusCode.BadRequest, "Day does not exist or it already has a dream");
                }
                var dream = new Dream
                {
                    Id = dreamRequest.Id,
                    Length = dreamRequest.Length,
                    Quality = dreamRequest.Quality,
                    Day = db.Days.FirstOrDefault(x => x.Id == dreamRequest.Id)
                };
                var _dream = db.Dreams.Add(dream);
                db.SaveChanges();
                return new ServiceResponse<DreamResponse>(new DreamResponse(_dream), HttpStatusCode.OK, "Dream added succesfully!");
            }
        }

        public ServiceResponse<DreamResponse> UpdateDream(UpdateDreamRequest updateDreamRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                Dream dreamToUpdate = db.Dreams.FirstOrDefault(x => x.Id == updateDreamRequest.Id);
                if (dreamToUpdate == null)
                {
                    return new ServiceResponse<DreamResponse>(null, HttpStatusCode.NotFound, "There is not existing dream with given id!");
                }
                dreamToUpdate = db.Dreams.FirstOrDefault(x => x.Id == updateDreamRequest.Id);
                if (updateDreamRequest.Length > 0)
                {
                    dreamToUpdate.Length = updateDreamRequest.Length;
                }
                if (!string.IsNullOrEmpty(updateDreamRequest.Quality))
                {
                    dreamToUpdate.Quality = updateDreamRequest.Quality;
                }
                db.SaveChanges();
                return new ServiceResponse<DreamResponse>(new DreamResponse(dreamToUpdate), HttpStatusCode.OK, "UserDetails added succesfully!");
            }
        }
    }
}
