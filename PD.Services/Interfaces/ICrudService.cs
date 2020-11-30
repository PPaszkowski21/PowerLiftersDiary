using PD.Services.Contracts.Api.UserDetails.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services
{
    public interface ICrudService<T>
    {
        ServiceResponse<T> Add(T content);
        ServiceResponse<IEnumerable<T>> Read();
        ServiceResponse<T> ReadById(int id);
        ServiceResponse<T> Update(T content);
        ServiceResponse Delete(int id);
    }
}
