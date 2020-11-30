using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
