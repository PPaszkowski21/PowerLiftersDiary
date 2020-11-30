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
        [Route("read")]
        public IHttpActionResult Read()
        {
            var result = _userService.Read();
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpGet]
        [Route("get")]
        public IHttpActionResult ReadById(int id)
        {
            if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _userService.ReadById(id);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _userService.Delete(id);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(UpdateUserRequest user)
        {
            if (user.Id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _userService.Update(user);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
    }
}