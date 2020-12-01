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
using System.Data.Entity;

namespace PD.Services.Services
{
    public class DiaryService : ICrudService<IDiary>
    {
        public ServiceResponse<IDiary> Add(IDiary diaryRequest)
        {
            Type myType = diaryRequest.GetType();
            PropertyInfo property = myType.GetProperty("UserId");
            int id = (int)property.GetValue(diaryRequest);
            using (DiaryContext db = new DiaryContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return new ServiceResponse<IDiary>(null, HttpStatusCode.NotFound, "Unable to find the user!");
                }

                var diary = new Diary
                {
                    User = user,
                    StartDate = diaryRequest.StartDate,
                    EndDate = diaryRequest.StartDate,
                    BenchPressStart = diaryRequest.BenchPressStart,
                    SquatStart = diaryRequest.SquatStart,
                    DeadliftStart = diaryRequest.DeadliftStart
                };
                Diary _diary = db.Diaries.Add(diary);
                db.SaveChanges();
                return new ServiceResponse<IDiary>(new DiaryResponse(_diary,typeof(DiaryResponse)), HttpStatusCode.OK, "Diary added succesfully!");
            }
        }

        public DiaryResponse GetDiary(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                Diary diary = db.Diaries.Include(x => x.Days).Include(x => x.User).FirstOrDefault(x => x.Id == id);
                if (diary == null)
                    return null;
                return new DiaryResponse(diary,typeof(DiaryResponse));
            }
        }

        public ServiceResponse Delete(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Diaries.Any(x => x.Id == id))
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "There is not existing user with given id!");
                }

                var diaryToRemove = db.Diaries.Include(x => x.Days).Include(x => x.User).FirstOrDefault(x => x.Id == id);
                foreach (var item in db.Days)
                {
                    Dream dream = db.Dreams.FirstOrDefault(x => x.Id == item.Id);
                    if (dream != null)
                    db.Dreams.Remove(dream);
                }
                db.Days.RemoveRange(diaryToRemove.Days);
                db.Diaries.Remove(diaryToRemove);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "User deleted!");
        }

        public ServiceResponse<IEnumerable<IDiary>> Read()
        {
            List<Diary> diaries = new List<Diary>();
            using (DiaryContext db = new DiaryContext())
            {
                diaries = db.Diaries.Include(x => x.Days).Include(x => x.User).ToList();
            }
            List<DiaryResponse> diaryResponses = new List<DiaryResponse>();
            foreach (var item in diaries)
            {
                diaryResponses.Add(new DiaryResponse(item,typeof(DiaryResponse)));
            }
            return new ServiceResponse<IEnumerable<IDiary>>(diaryResponses, HttpStatusCode.OK, "Table downloaded!");
        }

        public ServiceResponse<IDiary> ReadById(int id)
        {
            DiaryResponse diaryResponse = GetDiary(id);
            if(diaryResponse == null)
            {
                return new ServiceResponse<IDiary>(null, HttpStatusCode.NotFound, "There is not existing diary with given id!");
            }
            return new ServiceResponse<IDiary>(diaryResponse, HttpStatusCode.OK, "Diary downloaded!");
        }

        public ServiceResponse<IDiary> Update(IDiary updateDiaryRequest)
        {
            Type myType = updateDiaryRequest.GetType();
            PropertyInfo property = myType.GetProperty("Id");
            int id = (int)property.GetValue(updateDiaryRequest);
            Diary diaryToUpdate;
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Diaries.Any(x => x.Id == id))
                {
                    return new ServiceResponse<IDiary>(null, HttpStatusCode.NotFound, "There is not existing diary with given id!");
                }
                diaryToUpdate = db.Diaries.FirstOrDefault(x => x.Id == id);
                if (!string.IsNullOrEmpty(updateDiaryRequest.Conclusions))
                {
                    diaryToUpdate.Conclusions = updateDiaryRequest.Conclusions;
                }
                if (updateDiaryRequest.EndDate.Date.Year > 2019)
                {
                    diaryToUpdate.EndDate = updateDiaryRequest.EndDate;
                }
                if (updateDiaryRequest.StartDate.Date.Year > 2019)
                {
                    diaryToUpdate.StartDate = updateDiaryRequest.StartDate;
                }
                if (updateDiaryRequest.BenchPressStart != 0)
                {
                    diaryToUpdate.BenchPressStart = updateDiaryRequest.BenchPressStart;
                }
                if (updateDiaryRequest.SquatStart != 0)
                {
                    diaryToUpdate.SquatStart = updateDiaryRequest.SquatStart;
                }
                if (updateDiaryRequest.DeadliftStart != 0)
                {
                    diaryToUpdate.DeadliftStart = updateDiaryRequest.DeadliftStart;
                }
                if (updateDiaryRequest.BenchPressEnd != 0)
                {
                    diaryToUpdate.BenchPressEnd = updateDiaryRequest.BenchPressEnd;
                }
                if (updateDiaryRequest.SquatEnd != 0)
                {
                    diaryToUpdate.SquatEnd = updateDiaryRequest.SquatEnd;
                }
                if (updateDiaryRequest.DeadliftEnd != 0)
                {
                    diaryToUpdate.DeadliftEnd = updateDiaryRequest.DeadliftEnd;
                }
                if (updateDiaryRequest.Progress != 0)
                {
                    diaryToUpdate.Progress = updateDiaryRequest.Progress;
                }

                db.SaveChanges();
                return new ServiceResponse<IDiary>(new DiaryResponse(diaryToUpdate,typeof(DiaryResponse)), HttpStatusCode.OK, "User was updated successfully");
            }
        }
    }
}
