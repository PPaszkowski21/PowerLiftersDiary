using PD.Services.Contracts.Api.Diaries.Responses;
using PD.Services.Contracts.Api.UserDetails.Responses;
using PD.Services.Services;
using PowerlifterDiary.Models;
using System.Collections.Generic;

namespace PD.Services.Contracts.Api.Users.Responses
{
    public class UserResponse
    {
        public UserResponse()
        {

        }
        public UserResponse(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            City = user.City;
            if(user.UserDetails!=null)
            {
                //UserDetails = new UserDetailsResponse(user.UserDetails);
                UserService userService = new UserService();
                UserDetails = userService.ReadUserDetailsById(user.UserDetails.Id);
            }
            
            if(user.Diaries != null)
            {
                Diaries = new List<DiaryResponse>();
                DiaryService diaryService = new DiaryService();
                foreach (var diary in user.Diaries)
                {
                    Diaries.Add(diaryService.GetDiary(diary.Id));
                }
            }
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public UserDetailsResponse UserDetails { get; set; }
        public ICollection<DiaryResponse> Diaries { get; set; }
        //public string UserName { get; set; }
        //public string Password { get; set; }
    }
}
