using PD.Services.Contracts.Api.Exercises.Requests;
using PD.Services.Contracts.Api.ExercisesDetails.Request;
using PD.Services.Services;
using System.Net;
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

        [HttpPost]
        [Route("create/exerciseDetails")]
        public IHttpActionResult Create(AddExerciseDetailsRequest exerciseRequest)
        {
            if (exerciseRequest == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _exerciseService.Add(exerciseRequest);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
    }
}