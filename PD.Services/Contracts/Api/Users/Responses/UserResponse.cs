using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Users.Responses
{
    public class UserResponse : IUser
    {
        public UserResponse(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            City = user.City;
            UserDetails = user.UserDetails;
            Diaries = user.Diaries;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public virtual UserDetails UserDetails { get; set; }
        public ICollection<Diary> Diaries { get; set; }

        
    }
}
