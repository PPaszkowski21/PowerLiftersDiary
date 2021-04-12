using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Users.Requests
{
    public class AddAvatarRequest
    {
        [Required]
        public int UserDetailsId { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
