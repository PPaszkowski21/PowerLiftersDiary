namespace PD.Services.Interfaces
{
    public interface IUserDetails
    {
        int UserId { get; set; }
        int Height { get; set; }
        float Weight { get; set; }
        int Age { get; set; }
    }
}
