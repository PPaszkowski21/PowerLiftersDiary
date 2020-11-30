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
                return new ServiceResponse<IDiary>(null, HttpStatusCode.OK, "Diary added succesfully!");
            }
        }

        public DiaryResponse GetDiary(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Diaries.Any(x => x.Id == id))
                {
                    return null;
                }
                return new DiaryResponse(db.Diaries.Include(x=>x.Days).Include(x => x.User).FirstOrDefault(x => x.Id == id));
            }
        }

        public ServiceResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IEnumerable<IDiary>> Read()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IDiary> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IDiary> Update(IDiary content)
        {
            throw new NotImplementedException();
        }
    }
}
