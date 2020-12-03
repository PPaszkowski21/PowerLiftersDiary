using PD.Services.Contracts.Api.Exercises.Requests;
using PD.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace PD.Api.Controllers
{
    [RoutePrefix("exercise")]
    public class ExerciseController : BaseApiController
    {
        private readonly ExerciseService _exerciseService;
        public ExerciseController()
        {
            _exerciseService = new ExerciseService();
        }

        [HttpPost]
        [Route("create/exercise")]
        public IHttpActionResult Create(AddExerciseRequest exerciseRequest)
        {
            if (exerciseRequest == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _exerciseService.Add(exerciseRequest);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
    }
}