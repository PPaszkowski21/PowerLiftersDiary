using PD.Data.Models;
using PD.Services.Contracts.Api.Users.Responses;
using PowerlifterDiary.Models;
using UsersDetails = PowerlifterDiary.Models.UserDetails;

namespace PD.Services.Contracts.Api.UserDetails.Responses
{
    public class UserDetailsResponse
    {
        public int UserId { get; set; }
        public int Height { get; set; }
        public float Weight { get; set; }
        public int Age { get; set; }
        public float BMR { get; set; }
        public float BMI { get; set; }
        public string BrandingSettings { get; set; }
        public AvatarResponse Avatar { get; set; }
        public UserDetailsResponse()
        {

        }

        public UserDetailsResponse(UsersDetails userDetails)
        {
            UserId = userDetails.Id;
            Height = userDetails.Height;
            Weight = userDetails.Weight;
            Age = userDetails.Age;
            BMI = userDetails.BMI;
            BMR = userDetails.BMR;
            BrandingSettings = userDetails.BrandingSettings;
            if(userDetails.Avatar != null)
            {
                Avatar = new AvatarResponse(userDetails.Avatar);
            }
        }

        public UserDetailsResponse(User user)
        {
            if(user.UserDetails != null)
            {
                UserId = user.Id;
                Height = user.UserDetails.Height;
                Weight = user.UserDetails.Weight;
                Age = user.UserDetails.Age;
                BMR = user.UserDetails.BMR;
                BMI = user.UserDetails.BMI;
                BrandingSettings = user.UserDetails.BrandingSettings;
                if(user.UserDetails.Avatar != null)
                {
                    Avatar = new AvatarResponse(user.UserDetails.Avatar);
                }
            }
        }
    }
}
