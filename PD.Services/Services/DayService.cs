using PD.Services.Contracts.Api.Days.Requests;
using PD.Services.Contracts.Api.Days.Responses;
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
                return new ServiceResponse<IDay>(new DayResponse(_day), HttpStatusCode.OK, "Day added succesfully!");
            }
        }


        public ServiceResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IEnumerable<IDay>> Read()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IDay> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IDay> Update(IDay content)
        {
            throw new NotImplementedException();
        }



        //public ServiceResponse<IEnumerable<MonsterResponse>> Read()
        //{
        //    var monsters = new List<Monster>();
        //    var monsters2 = new List<MonsterResponse>();
        //    using (MonstersContext db = new MonstersContext())
        //    {
        //        monsters = db.Monsters.ToList();
        //    }
        //    foreach (var item in monsters)
        //    {
        //        monsters2.Add(new MonsterResponse(item));
        //    }
        //    return new ServiceResponse<IEnumerable<MonsterResponse>>(monsters2, HttpStatusCode.OK, "Table downloaded!");
        //}

        //public ServiceResponse<MonsterResponse> ReadById(int id)
        //{
        //    MonsterResponse monster;
        //    using (MonstersContext db = new MonstersContext())
        //    {
        //        if (!db.Monsters.Any(x => x.Id == id))
        //        {
        //            return new ServiceResponse<MonsterResponse>(null, HttpStatusCode.NotFound, "There is not existing monster with given id!");
        //        }
        //        monster = new MonsterResponse(db.Monsters.FirstOrDefault(x => x.Id == id));
        //    }
        //    return new ServiceResponse<MonsterResponse>(monster, HttpStatusCode.OK, "Monster downloaded!");
        //}

        //public ServiceResponse<MonsterResponse> Update(MonsterUpdateRequest monster)
        //{
        //    using (MonstersContext db = new MonstersContext())
        //    {
        //        try
        //        {
        //            var monsterToUpdate = db.Monsters.FirstOrDefault(x => x.Id == monster.Id);
        //            if (monster.HP.HasValue)
        //            {
        //                monsterToUpdate.HP = monster.HP.Value;
        //            }
        //            if (monster.Exp.HasValue)
        //            {
        //                monsterToUpdate.Exp = monster.Exp.Value;
        //            }
        //            if (!string.IsNullOrEmpty(monster.Name))
        //            {
        //                monsterToUpdate.Name = monster.Name;
        //            }
        //            if (monster.MovementSpeed.HasValue)
        //            {
        //                monsterToUpdate.MovementSpeed = monster.MovementSpeed.Value;
        //            }
        //            if (monster.SeeingInvisible != null)
        //            {
        //                monsterToUpdate.SeeingInvisible = monster.SeeingInvisible.Value;
        //            }
        //            if (!string.IsNullOrEmpty(monster.ImageLink))
        //            {
        //                monsterToUpdate.ImageLink = monster.ImageLink;
        //            }
        //            db.SaveChanges();
        //            return new ServiceResponse<MonsterResponse>(new MonsterResponse(monsterToUpdate), HttpStatusCode.OK, "Monster updated succesfully!");
        //        }
        //        catch (Exception ex)
        //        {
        //            return new ServiceResponse<MonsterResponse>(null, HttpStatusCode.NotFound, "There isn't existing monster with given id");
        //        }
        //    }
        //}
        //public ServiceResponse Delete(int id)
        //{
        //    using (MonstersContext db = new MonstersContext())
        //    {
        //        if (!db.Monsters.Any(x => x.Id == id))
        //        {
        //            return new ServiceResponse(HttpStatusCode.NotFound, "There is not existing monster with given id!");
        //        }
        //        db.Monsters.Remove(db.Monsters.FirstOrDefault(x => x.Id == id));
        //        db.SaveChanges();
        //    }
        //    return new ServiceResponse(HttpStatusCode.OK, "Monster deleted!");
        //}
    }
}
