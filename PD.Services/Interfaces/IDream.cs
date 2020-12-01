using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Interfaces
{
    public interface IDream
    {
        int Id { get; set; }
        float Length { get; set; }
        string Quality { get; set; }
    }
}
