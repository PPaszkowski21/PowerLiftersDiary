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

        public ServiceResponse<IUserDetails> AddDetails(IUserDetails userDetailsRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                User userVerification = db.Users.FirstOrDefault(x => x.Id == userDetailsRequest.UserId);
                if(userVerification == null || userVerification.UserDetails != null)
                {
                    return new ServiceResponse<IUserDetails>(null, HttpStatusCode.BadRequest, "User does not exist or it already has a details");
                }
                var userDetails = new UserDetails
                {
                    Id = userDetailsRequest.UserId,
                    Age = userDetailsRequest.Age,
                    Height = userDetailsRequest.Height,
                    Weight = userDetailsRequest.Weight,
                    User = db.Users.FirstOrDefault(x => x.Id == userDetailsRequest.UserId)
                };
                var BMIandBMR = CalculateBMIandBMR(userDetails.Weight, userDetails.Height, userDetails.Height);
                userDetails.BMI = BMIandBMR[0];
                userDetails.BMR = BMIandBMR[1];
                var _user = db.UserDetails.Add(userDetails);
                db.SaveChanges();
                return new ServiceResponse<IUserDetails>(new UserDetailsResponse(_user), HttpStatusCode.OK, "UserDetails added succesfully!");
            }
        }

        public ServiceResponse<IUserDetails> UpdateDetails(IUserDetails userDetailsRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                UserDetails userDetailsToUpdate = db.UserDetails.FirstOrDefault(x => x.Id == userDetailsRequest.UserId);
                if (userDetailsToUpdate == null)
                {
                    return new ServiceResponse<IUserDetails>(null, HttpStatusCode.NotFound, "There are not existing user details with given id!");
                }
                userDetailsToUpdate = db.UserDetails.FirstOrDefault(x => x.Id == userDetailsRequest.UserId);
                if (userDetailsRequest.Age > 0)
                {
                    userDetailsToUpdate.Age = userDetailsRequest.Age;
                }
                if (userDetailsRequest.Height > 0)
                {
                    userDetailsToUpdate.Height = userDetailsRequest.Height;
                }
                if (userDetailsRequest.Weight > 0)
                {
                    userDetailsToUpdate.Weight = userDetailsRequest.Weight;
                }
                db.SaveChanges();
                return new ServiceResponse<IUserDetails>(new UserDetailsResponse(userDetailsToUpdate), HttpStatusCode.OK, "UserDetails added succesfully!");
            }
        }
        public float[] CalculateBMIandBMR(float weight, int height, int age)
        {
            float[] results = new float[2];
            double heightToSquare = Convert.ToDouble(height);
            float height2 = Convert.ToSingle(Math.Pow(heightToSquare, 2))/10000;
            results[0] = weight / height2;
            results[1] = (float)9.99 * weight + (float)6.25 * height - (float)4.92 * age + 5;
            return results;
        }
    }
}
