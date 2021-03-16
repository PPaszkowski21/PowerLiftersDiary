using PD.Services.Contracts.Api.Exercises.Requests;
using PD.Services.Contracts.Api.ExercisesDetails.Request;
using PD.Services.Contracts.Api.ExercisesEquipments.Requests;
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

        [HttpPost]
        [Route("create/exerciseEquipment")]
        public IHttpActionResult Create(AddExerciseEquipmentRequest exerciseRequest)
        {
            if (exerciseRequest == null || !ModelState.IsValid) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _exerciseService.Add(exerciseRequest);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpDelete]
        [Route("delete/exercise")]
        public IHttpActionResult DeleteExercise(int id)
        {
            if (id <= 0) return ResponseMessage(CreateCustomResponseMessage(HttpStatusCode.BadRequest));
            var result = _exerciseService.DeleteExercise(id);
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpGet]
        [Route("getall/exerciseequipment")]
        public IHttpActionResult ReadExEquipment()
        {
            var result = _exerciseService.GetAllExerciseEquipment();
            return ResponseMessage(CreateCustomResponseMessage(result));
        }

        [HttpGet]
        [Route("getall/exercisesdetails")]
        public IHttpActionResult ReadExDetails()
        {
            var result = _exerciseService.GetAllExerciseDetails();
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
        [HttpGet]
        [Route("getall/exercises")]
        public IHttpActionResult ReadEx()
        {
            var result = _exerciseService.GetAllExercises();
            return ResponseMessage(CreateCustomResponseMessage(result));
        }
        //[HttpGet]
        //[Route("checkschedule")]
        //public IHttpActionResult CheckTrainingSchedule()
        //{
        //    var result = _exerciseService;
        //}
    }
}