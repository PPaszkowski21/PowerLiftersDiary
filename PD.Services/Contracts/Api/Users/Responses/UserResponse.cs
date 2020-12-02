using PD.Services.Contracts.Api.Diaries.Responses;
using PD.Services.Contracts.Api.UserDetails.Responses;
using PD.Services.Interfaces;
using PD.Services.Services;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Users.Responses
{
    public class UserResponse : ICustomUser
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
            UserDetails = new UserDetailsResponse(user);
            Diaries = new List<DiaryResponse>();
            if(user.Diaries != null)
            {
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
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
