using PD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.UserDetails.Requests
{
    public class AddUserDetailsRequest : IUserDetails
    {
        [Required]
        public  int UserId { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public int Age { get; set; }
        public float BMR { get; set; }
        public float BMI { get; set; }
    }
}
