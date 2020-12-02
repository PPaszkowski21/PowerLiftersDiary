using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Interfaces
{
    public interface ICustomUser
    {
        string UserName { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string City { get; set; }
        string Password { get; set; }
    }
}
