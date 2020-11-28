using PD.Services;
using PD.Services.Contracts.Api.Users.Requests;
using PD.Services.Interfaces;
using PD.Services.Services;
using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace PD.Api.Controllers
{
    [RoutePrefix("user")]
    public class UserController : BaseApiController
    {
        private readonly ICrudService<IUser> _userService;
        public UserController()
        {
            this._userService = new UserService();
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(AddUserRequest user)
        {
            if (user == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _userService.Add(user);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpGet]
        [Route("get")]
        public User GetUser(int id)
        {
            using(DiaryContext db = new DiaryContext())
            {
                User user = db.Users.Where(x => x.Id == id).Include(x => x.Diaries).FirstOrDefault();
                return user;
            }
        }
    }
}