using PD.Services.Contracts.Api.Users.Responses;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.UserDetails.Responses
{
    public class UserDetailsResponse
    {
        public int Owner { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public int Age { get; set; }
        public float BMR { get; set; }
        public float BMI { get; set; }
        public UserResponse User { get; set; }
        public UserDetailsResponse()
        {

        }

        public UserDetailsResponse(User user)
        {
            if(user.UserDetails != null)
            {
                Owner = user.Id;
                Height = user.UserDetails.Height;
                Weight = user.UserDetails.Weight;
                Age = user.UserDetails.Age;
                BMR = user.UserDetails.BMR;
                BMI = user.UserDetails.BMI;
            }
        }
    }
}
