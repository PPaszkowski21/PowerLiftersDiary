using PD.Services.Contracts.Api.UserDetails.Responses;
using PD.Services.Contracts.Api.Users.Responses;
using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;

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
                UserDetails = null
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

                var userToRemove = db.Users.Include(x => x.Diaries).Include(x => x.UserDetails).FirstOrDefault(x => x.Id == id);
                db.Diaries.RemoveRange(userToRemove.Diaries);
                if(userToRemove.UserDetails != null)
                {
                    db.UserDetails.Remove(userToRemove.UserDetails);
                }
                db.Users.Remove(userToRemove);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "User deleted!");
        }

        public ServiceResponse<IEnumerable<IUser>> Read()
        {
            List<User> users = new List<User>();
            using (DiaryContext db = new DiaryContext())
            {
                users = db.Users.Include(x => x.Diaries).Include(x => x.UserDetails).ToList();
            }
            List<UserResponse> userResponses = new List<UserResponse>();
            foreach (var item in users)
            {
                userResponses.Add(new UserResponse(item));
            }
            return new ServiceResponse<IEnumerable<IUser>>(userResponses, HttpStatusCode.OK, "Table downloaded!");
        }

        public ServiceResponse<IUser> ReadById(int id)
        {
            User user;
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Users.Any(x => x.Id == id))
                {
                    return new ServiceResponse<IUser>(null, HttpStatusCode.NotFound, "There is not existing user with given id!");
                }
                user = db.Users.Include(x=>x.Diaries).Include(x=>x.UserDetails).FirstOrDefault(x => x.Id == id); 
            }
            UserResponse userResponse = new UserResponse(user);
            return new ServiceResponse<IUser>(userResponse, HttpStatusCode.OK, "User downloaded!");
        }

        public ServiceResponse<IUser> Update(IUser updateUserRequest)
        {
            Type myType = updateUserRequest.GetType();
            PropertyInfo property = myType.GetProperty("Id");
            int id = (int)property.GetValue(updateUserRequest);
            User userToUpdate;
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Users.Any(x => x.Id == id))
                {
                    return new ServiceResponse<IUser>(null, HttpStatusCode.NotFound, "There is not existing user with given id!");
                }
                userToUpdate = db.Users.FirstOrDefault(x => x.Id == id);
                if (!string.IsNullOrEmpty(updateUserRequest.Name))
                {
                    userToUpdate.Name = updateUserRequest.Name;
                }
                if (!string.IsNullOrEmpty(updateUserRequest.Surname))
                {
                    userToUpdate.Surname = updateUserRequest.Surname;
                }
                if (!string.IsNullOrEmpty(updateUserRequest.City))
                {
                    userToUpdate.City = updateUserRequest.City;
                }
                db.SaveChanges();
                return new ServiceResponse<IUser>(new UserResponse(userToUpdate), HttpStatusCode.OK, "User was updated successfully");
            }

        }

    }
}
