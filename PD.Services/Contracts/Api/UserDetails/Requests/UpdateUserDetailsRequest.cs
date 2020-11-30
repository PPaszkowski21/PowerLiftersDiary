using PD.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PD.Services.Contracts.Api.UserDetails.Requests
{
    public class UpdateUserDetailsRequest : IUserDetails
    {
        [Required]
        public int UserId { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public int Age { get; set; }
    }
}
