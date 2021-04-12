using PD.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.Users.Responses
{
    public class AvatarResponse
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public AvatarResponse(Avatar avatar)
        {
            Id = avatar.Id;
            Image = Convert.ToBase64String(avatar.Image);
        }
    }
}
