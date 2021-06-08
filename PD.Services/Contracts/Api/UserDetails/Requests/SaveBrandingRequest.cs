using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.UserDetails.Requests
{
    public class SaveBrandingRequest
    {
        public int Id { get; set; }
        public string BrandingSettings { get; set; }
    }
}
