using PD.Services.Contracts.Api.Users.Responses;
using PD.Services.Interfaces;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Services
{
    public class UserService : ICrudService<IUser>
    {
        public ServiceResponse<IUser> Add(IUser userRequest)
        {
            var user = new User
            {
                Name = userRequest.Name,
                Surname = userRequest.Surname,
                City = userRequest.City
            };
            using (DiaryContext db = new DiaryContext())
            {
                var _user = db.Users.Add(user);
                db.SaveChanges();
                return new ServiceResponse<IUser>(new UserResponse(_user), HttpStatusCode.OK, "User added succesfully!");
            }
        }

        public ServiceResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IEnumerable<IUser>> Read()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IUser> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<IUser> Update(IUser content)
        {
            throw new NotImplementedException();
        }
    }
}
