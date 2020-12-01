using PD.Services.Contracts.Api.Days.Requests;
using PD.Services.Contracts.Api.Days.Responses;
using PD.Services.Contracts.Api.Diaries.Responses;
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
                db.TrainingUnits.RemoveRange(dayToRemove.TrainingUnits);
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
                //User dreamVerification = db.Users.FirstOrDefault(x => x.Id == userDetailsRequest.UserId);
                //if (userVerification == null || userVerification.UserDetails != null)
                //{
                //    return new ServiceResponse<IDream>(null, HttpStatusCode.BadRequest, "User does not exist or it already has a details");
                //}
                //var userDetails = new UserDetails
                //{
                //    Id = userDetailsRequest.UserId,
                //    Age = userDetailsRequest.Age,
                //    Height = userDetailsRequest.Height,
                //    Weight = userDetailsRequest.Weight,
                //    User = db.Users.FirstOrDefault(x => x.Id == userDetailsRequest.UserId)
                //};
                //var BMIandBMR = CalculateBMIandBMR(userDetails.Weight, userDetails.Height, userDetails.Height);
                //userDetails.BMI = BMIandBMR[0];
                //userDetails.BMR = BMIandBMR[1];
                //var _user = db.UserDetails.Add(userDetails);
                //db.SaveChanges();
                return new ServiceResponse<IDream>(null, HttpStatusCode.OK, "UserDetails added succesfully!");
            }
        }

        public ServiceResponse<IDream> UpdateDream(IDream userDetailsRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                //UserDetails userDetailsToUpdate = db.UserDetails.FirstOrDefault(x => x.Id == userDetailsRequest.UserId);
                //if (userDetailsToUpdate == null)
                //{
                //    return new ServiceResponse<IDream>(null, HttpStatusCode.NotFound, "There are not existing user details with given id!");
                //}
                //userDetailsToUpdate = db.UserDetails.FirstOrDefault(x => x.Id == userDetailsRequest.UserId);
                //if (userDetailsRequest.Age > 0)
                //{
                //    userDetailsToUpdate.Age = userDetailsRequest.Age;
                //}
                //if (userDetailsRequest.Height > 0)
                //{
                //    userDetailsToUpdate.Height = userDetailsRequest.Height;
                //}
                //if (userDetailsRequest.Weight > 0)
                //{
                //    userDetailsToUpdate.Weight = userDetailsRequest.Weight;
                //}
                //db.SaveChanges();
                return new ServiceResponse<IDream>(null, HttpStatusCode.OK, "UserDetails added succesfully!");
            }
        }
    }
}
