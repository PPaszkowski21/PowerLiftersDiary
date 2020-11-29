using PD.Services.Contracts.Api.UserDetails.Responses;
using PD.Services.Contracts.Api.Users.Responses;
using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;

namespace PD.Services.Services
{
    public class UserService : ICrudService<IUser>
    {
        public ServiceResponse<IUser> Add(IUser userRequest)
        {
            var user = new User
            {
                Name = userRequest.Name,
                Surname = userRequest.Surname,
                City = userRequest.City,
                UserDetails = new UserDetails()
            };
            using (DiaryContext db = new DiaryContext())
            {
                var _user = db.Users.Add(user);
                db.SaveChanges();
                return new ServiceResponse<IUser>(new UserResponse(_user), HttpStatusCode.OK, "User added succesfully!");
            }
        }

        public ServiceResponse Delete(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Users.Any(x => x.Id == id))
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "There is not existing user with given id!");
                }
                db.Users.Remove(db.Users.FirstOrDefault(x => x.Id == id));
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "User deleted!");
        }

        public ServiceResponse<IEnumerable<IUser>> Read()
        {
            var users = new List<User>();
            var users2 = new List<UserResponse>();
            //using (DiaryContext db = new DiaryContext())
            //{
            //    users = db.Users.Include("Diaries","UserDetails").ToList();
            //    var users2 = db.Users.Select(o => new UserResponse
            //    {
            //        Id = o.,
            //        Diaries = o..Select(ot => ot.).ToList()
            //    }).ToList();
            //}
            //foreach (var item in users)
            //{
            //    users2.Add(new UserResponse(item));
            //}
            return new ServiceResponse<IEnumerable<IUser>>(users2, HttpStatusCode.OK, "Table downloaded!");
        }

        public ServiceResponse<IUser> ReadById(int id)
        {
            User user;
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Users.Any(x => x.Id == id))
                {
                    return new ServiceResponse<IUser>(null, HttpStatusCode.NotFound, "There is not existing monster with given id!");
                }
                user = db.Users.Include(x=>x.Diaries).Include(x=>x.UserDetails).FirstOrDefault(x => x.Id == id); 
            }
            UserResponse userResponse = new UserResponse(user);
            return new ServiceResponse<IUser>(userResponse, HttpStatusCode.OK, "Monster downloaded!");
        }
            public ServiceResponse<IUser> Update(IUser content)
            {
                throw new NotImplementedException();
            }
    }
}
