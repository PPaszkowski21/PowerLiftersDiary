using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using PD.Data.Models;
using PD.Services.Contracts.Api.UserDetails.Requests;
using PD.Services.Contracts.Api.UserDetails.Responses;
using PD.Services.Contracts.Api.Users.Requests;
using PD.Services.Contracts.Api.Users.Responses;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PD.Services.Services
{
    public class UserService
    {
        public ServiceResponse<UserResponse> Add(AddUserRequest userRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                var user = new User
                {
                    UserName = userRequest.UserName,
                    Name = userRequest.Name,
                    Surname = userRequest.Surname,
                    City = userRequest.City,
                };

                var userManager = new UserManager<User, int>(new UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(db));

                userManager.Create(user, userRequest.Password);

                var userAfterAdd = userManager.FindByName(userRequest.UserName);

                return new ServiceResponse<UserResponse>(new UserResponse(userAfterAdd), HttpStatusCode.OK, "User added succesfully!");
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
                DiaryService diaryService = new DiaryService();
                foreach (var diary in userToRemove.Diaries)
                {
                    diaryService.Delete(diary.Id);
                }
                if (userToRemove.UserDetails != null)
                {
                    db.UserDetails.Remove(userToRemove.UserDetails);
                }
                db.SaveChanges();
            }
            using (DiaryContext db = new DiaryContext())
            {
                var userToRemove = db.Users.Include(x => x.Diaries).Include(x => x.UserDetails).FirstOrDefault(x => x.Id == id);
                var userManager = new UserManager<User, int>(new UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(db));
                userManager.Delete(userToRemove);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "User deleted!");
        }

        //public async ServiceResponse DeleteUser(int id)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByIdAsync(id);
        //        if (user == null)
        //            return new ServiceResponse
        //            {
        //                ResponseMessage = "Unable to get user.",
        //                StatusCode = HttpStatusCode.NotFound
        //            };

        //        var result = await _userManager.DeleteAsync(user);
        //        if (!result.Succeeded)
        //            return new ServiceResponse
        //            {
        //                ResponseMessage = "Unable to delete user.",
        //                StatusCode = HttpStatusCode.InternalServerError
        //            };

        //        return new ServiceResponse
        //        {
        //            StatusCode = HttpStatusCode.OK
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO log errors
        //        return new ServiceResponse
        //        {
        //            StatusCode = HttpStatusCode.InternalServerError,
        //            ResponseMessage = "Unhandled error."
        //        };
        //    }
        //}

        //public ServiceResponse<IEnumerable<UserResponse>> Read()
        //{
        //    List<User> users = new List<User>();
        //    using (DiaryContext db = new DiaryContext())
        //    {
        //        users = db.Users.Include(x => x.Diaries).Include(x => x.UserDetails).ToList();
        //    }
        //    List<UserResponse> userResponses = new List<UserResponse>();
        //    foreach (var item in users)
        //    {
        //        userResponses.Add(new UserResponse(item));
        //    }
        //    return new ServiceResponse<IEnumerable<UserResponse>>(userResponses, HttpStatusCode.OK, "Table downloaded!");
        //}

        public ServiceResponse<UserResponse> ReadById(int id)
        {
            User user;
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Users.Any(x => x.Id == id))
                {
                    return new ServiceResponse<UserResponse>(null, HttpStatusCode.NotFound, "There is not existing user with given id!");
                }
                user = db.Users.Include(x => x.Diaries).Include(x => x.UserDetails).FirstOrDefault(x => x.Id == id);
            }
            UserResponse userResponse = new UserResponse(user);
            return new ServiceResponse<UserResponse>(userResponse, HttpStatusCode.OK, "User downloaded!");
        }

        public UserDetailsResponse ReadUserDetailsById(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                UserDetails userDetails = db.UserDetails.Include(x => x.Avatar).FirstOrDefault(x => x.Id == id);
                if (userDetails == null)
                    return null;
                return new UserDetailsResponse(userDetails);
            }
        }
        public ServiceResponse<UserResponse> Update(UpdateUserRequest updateUserRequest)
        {
            User userToUpdate;
            using (DiaryContext db = new DiaryContext())
            {
                if (!db.Users.Any(x => x.Id == updateUserRequest.Id))
                {
                    return new ServiceResponse<UserResponse>(null, HttpStatusCode.NotFound, "There is not existing user with given id!");
                }
                userToUpdate = db.Users.FirstOrDefault(x => x.Id == updateUserRequest.Id);
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
                return new ServiceResponse<UserResponse>(new UserResponse(userToUpdate), HttpStatusCode.OK, "User was updated successfully");
            }
        }

        public ServiceResponse<UserDetailsResponse> AddDetails(AddUserDetailsRequest userDetailsRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                User userVerification = db.Users.FirstOrDefault(x => x.Id == userDetailsRequest.UserId);
                if (userVerification == null || userVerification.UserDetails != null)
                {
                    return new ServiceResponse<UserDetailsResponse>(null, HttpStatusCode.BadRequest, "User does not exist or it already has a details");
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
                return new ServiceResponse<UserDetailsResponse>(new UserDetailsResponse(_user), HttpStatusCode.OK, "UserDetails added succesfully!");
            }
        }

        public ServiceResponse<UserDetailsResponse> UpdateDetails(UpdateUserDetailsRequest userDetailsRequest)
        {
            using (DiaryContext db = new DiaryContext())
            {
                UserDetails userDetailsToUpdate = db.UserDetails.FirstOrDefault(x => x.Id == userDetailsRequest.UserId);

                if (userDetailsToUpdate == null)
                {
                    return new ServiceResponse<UserDetailsResponse>(null, HttpStatusCode.NotFound, "There are not existing user details with given id!");
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
                float[] results = CalculateBMIandBMR(userDetailsToUpdate.Weight, userDetailsToUpdate.Height, userDetailsToUpdate.Age);
                userDetailsToUpdate.BMI = results[0];
                userDetailsToUpdate.BMR = results[1];
                db.SaveChanges();
                return new ServiceResponse<UserDetailsResponse>(new UserDetailsResponse(userDetailsToUpdate), HttpStatusCode.OK, "UserDetails added succesfully!");
            }
        }

        public ServiceResponse<UserResponse> GetUserModelFromRequest(HttpRequestMessage request)
        {
            if (request.Properties.TryGetValue("Ticket", out var value))
            {
                if (value is AuthenticationTicket mappedTicket)
                {
                    if (mappedTicket.Properties.Dictionary.TryGetValue("UserId", out string id))
                    {
                        if (int.TryParse(id, out var parsedId ))
                        {
                            using (var db = new DiaryContext())
                            using (var userManager = new UserManager<User, int>(new UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(db)))
                            {
                                var user = userManager.FindById(parsedId);
                                return new ServiceResponse<UserResponse>(new UserResponse(user), HttpStatusCode.OK, "User returned successfully");
                            }
                        }
                    }
                }
            }
            return null;
        }

        public ServiceResponse<AvatarResponse> AddAvatar(AddAvatarRequest request)
        {
            Avatar avatar;
            using (DiaryContext db = new DiaryContext())
            {
                var userDetails = db.UserDetails.FirstOrDefault(x => x.Id == request.UserDetailsId);
                if (userDetails == null)
                {
                    return new ServiceResponse<AvatarResponse>(null, HttpStatusCode.NotFound, "Unable to find the user details!");
                }
                avatar = db.Avatars.FirstOrDefault(x => x.Id == userDetails.Id);
                if(avatar != null)
                {
                    avatar.Image = Convert.FromBase64String(request.Image);
                }
                else
                {
                    avatar = new Avatar()
                    {
                        Id = userDetails.Id,
                        Image = Convert.FromBase64String(request.Image),
                        UserDetails = userDetails
                    };
                    db.Avatars.Add(avatar);
                }
                db.SaveChanges();
            }
            return new ServiceResponse<AvatarResponse>(new AvatarResponse(avatar),HttpStatusCode.OK, "Avatar created successfully");
        }
        public ServiceResponse<AvatarResponse> GetAvatar(int id)
        {
            Avatar avatar;
            using(DiaryContext db = new DiaryContext())
            {
                avatar = db.Avatars.FirstOrDefault(x => x.Id == id);
                if(avatar == null)
                {
                    return new ServiceResponse<AvatarResponse>(null, HttpStatusCode.NotFound, "There is no existing avatar with given id!");
                }
            }
            return new ServiceResponse<AvatarResponse>(new AvatarResponse(avatar), HttpStatusCode.OK, "Avatar downloaded");
        }

        public ServiceResponse DeleteAvatar(int id)
        {
            using (DiaryContext db = new DiaryContext())
            {
                if(!db.Avatars.Any(x=>x.Id == id))
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "There is no existing avatar with given id!");
                }
                Avatar avatarToRemove = db.Avatars.FirstOrDefault(x => x.Id == id);
                db.Avatars.Remove(avatarToRemove);
                db.SaveChanges();
            }
            return new ServiceResponse(HttpStatusCode.OK, "Avatar deleted!");
        }
        private float[] CalculateBMIandBMR(float weight, int height, int age)
        {
            float[] results = new float[2];
            double heightToSquare = Convert.ToDouble(height);
            float height2 = Convert.ToSingle(Math.Pow(heightToSquare, 2)) / 10000;
            results[0] = weight / height2;
            results[1] = (float)9.99 * weight + (float)6.25 * height - (float)4.92 * age + 5;
            return results;
        }
    }
}
