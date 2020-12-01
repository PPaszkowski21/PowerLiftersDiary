using PD.Services.Contracts.Api.Days.Requests;
using PD.Services.Contracts.Api.Days.Responses;
using PD.Services.Contracts.Api.Diaries.Responses;
using PD.Services.Contracts.Api.Dreams.Responses;
using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Services
{
    public class DayService : ICrudService<IDay>
    {
        public ServiceResponse<IDay> Add(IDay dayRequest)
        {
            Type myType = dayRequest.GetType();
            PropertyInfo property = myType.GetProperty("DiaryId");
            int id = (int)property.GetValue(dayRequest);
            using (DiaryContext db = new DiaryContext())
            {
                var diary = db.Diaries.FirstOrDefault(x => x.Id == id);
                if (diary == null)
                {
                    return new ServiceResponse<IDay>(null, HttpStatusCode.NotFound, "Unable to find the diary!");
                }

                var day = new Day
                {
                    Date = dayRequest.Date,
                    Diary = diary,
                };
                Day _day = db.Days.Add(day);
                db.SaveChanges();
                return new ServiceResponse<IDay>(new DayResponse(_day,typeof(DayResponse)), HttpStatusCode.OK, "Day added succesfully!");
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
                //db.TrainingUnits.RemoveRange(dayToRemove.TrainingUnits);
                db.Dreams.Remove(dayToRemove.Dream);
                db.Days.Remove(dayToRemove);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "Day deleted!");
        }

        public ServiceResponse<IEnumerable<IDay>> Read()
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
            return new ServiceResponse<IEnumerable<IDay>>(dayResponses, HttpStatusCode.OK, "Table downloaded!");
        }

        public ServiceResponse<IDay> ReadById(int id)
        {
            DayResponse dayResponse = GetDay(id);
            if (dayResponse == null)
            {
                return new ServiceResponse<IDay>(null, HttpStatusCode.NotFound, "There is not existing day with given id!");
            }
            return new ServiceResponse<IDay>(dayResponse, HttpStatusCode.OK, "Diary downloaded!");
        }
        public ServiceResponse<IDay> Update(IDay updateDayRequest)
        {
            Type myType = updateDayRequest.GetType();
            PropertyInfo property = myType.GetProperty("Id");
            int id = (int)property.GetValue(updateDayRequest);
            Day dayToUpdate;
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Days.Any(x => x.Id == id))
                {
                    return new ServiceResponse<IDay>(null, HttpStatusCode.NotFound, "There is not existing day with given id!");
                }
                dayToUpdate = db.Days.FirstOrDefault(x => x.Id == id);
                if (updateDayRequest.Date.Year > 2019)
                {
                    dayToUpdate.Date = updateDayRequest.Date;
                }
                db.SaveChanges();
                return new ServiceResponse<IDay>(new DayResponse(dayToUpdate,typeof(DayResponse)), HttpStatusCode.OK, "User was updated successfully");
            }
        }

        public ServiceResponse<IDream> AddDream(IDream dreamRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var dreamVerification = db.Days.FirstOrDefault(x => x.Id == dreamRequest.Id);
                if (dreamVerification == null || dreamVerification.Dream != null)
                {
                    return new ServiceResponse<IDream>(null, HttpStatusCode.BadRequest, "Day does not exist or it already has a dream");
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
                return new ServiceResponse<IDream>(new DreamResponse(_dream,typeof(DreamResponse)), HttpStatusCode.OK, "Dream added succesfully!");
            }
        }

        public ServiceResponse<IDream> UpdateDream(IDream updateDreamRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                Dream dreamToUpdate = db.Dreams.FirstOrDefault(x => x.Id == updateDreamRequest.Id);
                if (dreamToUpdate == null)
                {
                    return new ServiceResponse<IDream>(null, HttpStatusCode.NotFound, "There is not existing dream with given id!");
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
                return new ServiceResponse<IDream>(new DreamResponse(dreamToUpdate,typeof(DreamResponse)), HttpStatusCode.OK, "UserDetails added succesfully!");
            }
        }
    }
}
