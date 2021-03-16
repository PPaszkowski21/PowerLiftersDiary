using PD.Data.Models;
using PD.Services.Contracts.Api.Days.Responses;
using PD.Services.Contracts.Api.Diaries.Requests;
using PD.Services.Contracts.Api.Diaries.Responses;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;

namespace PD.Services.Services
{
    public class DiaryService
    {
        public Dictionary<string, int> BodyPartToDo = new Dictionary<string, int>()
            {
                {"Klatka",4},
                {"Plecy",4},
                {"Nogi",4},
                {"Barki",3},
                {"Biceps",3},
                {"Triceps",3},
                {"Brzuch", 3}
            };
        public ServiceResponse<DiaryResponse> Add(AddDiaryRequest diaryRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Id == diaryRequest.UserId);
                if (user == null)
                {
                    return new ServiceResponse<DiaryResponse>(null, HttpStatusCode.NotFound, "Unable to find the user!");
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
                return new ServiceResponse<DiaryResponse>(new DiaryResponse(_diary), HttpStatusCode.OK, "Diary added succesfully!");
            }
        }

        public DiaryResponse GetDiary(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                Diary diary = db.Diaries.Include(x => x.Days).Include(x => x.User).FirstOrDefault(x => x.Id == id);
                if (diary == null)
                    return null;
                return new DiaryResponse(diary);
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
                DayService dayService = new DayService();
                foreach (var day in diaryToRemove.Days)
                {
                    dayService.Delete(day.Id);
                }
                db.SaveChanges();
            }
            using (DiaryContext db = new DiaryContext())
            {
                var diaryToRemove2 = db.Diaries.Include(x => x.Days).Include(x => x.User).FirstOrDefault(x => x.Id == id);
                db.Diaries.Remove(diaryToRemove2);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "User deleted!");
        }

        //public ServiceResponse<IEnumerable<DiaryResponse>> Read()
        //{
        //    List<Diary> diaries = new List<Diary>();
        //    using (DiaryContext db = new DiaryContext())
        //    {
        //        diaries = db.Diaries.Include(x => x.Days).Include(x => x.User).ToList();
        //    }
        //    List<DiaryResponse> diaryResponses = new List<DiaryResponse>();
        //    foreach (var item in diaries)
        //    {
        //        diaryResponses.Add(new DiaryResponse(item));
        //    }
        //    return new ServiceResponse<IEnumerable<DiaryResponse>>(diaryResponses, HttpStatusCode.OK, "Table downloaded!");
        //}

        //public ServiceResponse<DiaryResponse> ReadById(int id)
        //{
        //    DiaryResponse diaryResponse = GetDiary(id);
        //    if(diaryResponse == null)
        //    {
        //        return new ServiceResponse<DiaryResponse>(null, HttpStatusCode.NotFound, "There is not existing diary with given id!");
        //    }
        //    return new ServiceResponse<DiaryResponse>(diaryResponse, HttpStatusCode.OK, "Diary downloaded!");
        //}

        public ServiceResponse<DiaryResponse> Update(UpdateDiaryRequest updateDiaryRequest)
        {
            Diary diaryToUpdate;
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Diaries.Any(x => x.Id == updateDiaryRequest.Id))
                {
                    return new ServiceResponse<DiaryResponse>(null, HttpStatusCode.NotFound, "There is not existing diary with given id!");
                }
                diaryToUpdate = db.Diaries.FirstOrDefault(x => x.Id == updateDiaryRequest.Id);
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
                return new ServiceResponse<DiaryResponse>(new DiaryResponse(diaryToUpdate), HttpStatusCode.OK, "User was updated successfully");
            }
        }

        public ServiceResponse<WeekSummaryResponse> WeekSummary(int diaryId, int dayId)
        {
            DiaryResponse diary = GetDiary(diaryId);
            DayService dayService = new DayService();
            DayResponse day = dayService.GetDay(dayId);
            while (day.Date.DayOfWeek != DayOfWeek.Monday)
            {
                day.Date = day.Date.AddDays(-1);
            }
            DateTime startOfTheWeek = day.Date;
            DateTime endOfTheWeek = day.Date.AddDays(6);
            string dateRange = startOfTheWeek.ToString() + " -> " + endOfTheWeek.ToString();
            List<DayResponse> days = diary.Days.Where(x => x.Date.Ticks >= startOfTheWeek.Ticks && x.Date.Ticks <= endOfTheWeek.Ticks).ToList();
            List<ExerciseStatus> weekSummary = new List<ExerciseStatus>();

            foreach (var singleDay in days)
            {
                foreach (var trainingUnit in day.TrainingUnits)
                {
                    foreach (var exercise in trainingUnit.ExerciseTrainings)
                    {
                        if (!weekSummary.Any(x => x.BodyPart == exercise.Exercise.BodyPart))
                        {
                            ExerciseStatus exerciseStatus = new ExerciseStatus
                            {
                                BodyPart = exercise.Exercise.BodyPart,
                                ExercisesToDo = BodyPartToDo[exercise.Exercise.BodyPart]
                            };
                            weekSummary.Add(exerciseStatus);
                        }
                        else
                        {
                            weekSummary.FirstOrDefault(x => x.BodyPart == exercise.Exercise.BodyPart).ExercisesDone++;
                        }
                    }
                }
            }
            return new ServiceResponse<WeekSummaryResponse>(new WeekSummaryResponse(dateRange, weekSummary), HttpStatusCode.OK, "Week summary done successfully");
        }

    }
}
