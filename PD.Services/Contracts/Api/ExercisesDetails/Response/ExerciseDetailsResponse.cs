using PowerlifterDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Services.Contracts.Api.ExercisesDetails.Response
{
    public class ExerciseDetailsResponse
    {
        public int Id { get; set; }
        public float Eccentric { get; set; }
        public int EccentricPause { get; set; }
        public float Concetric { get; set; }
        public int ConcetricPause { get; set; }
        public int Series { get; set; }
        public int Repeats { get; set; }
        public ExerciseDetailsResponse(ExerciseDetails exercise)
        {
            Id = exercise.Id;
            Eccentric = exercise.Eccentric;
            EccentricPause = exercise.EccentricPause;
            Concetric = exercise.Concetric;
            ConcetricPause = exercise.ConcetricPause;
            Series = exercise.Series;
            Repeats = exercise.Repeats;

        }
    }
}
