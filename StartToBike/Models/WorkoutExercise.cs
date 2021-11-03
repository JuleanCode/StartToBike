using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartToBike.Models
{
    public class WorkoutExercise
    {
        public int WorkoutID { get; set; }
        public Workout Workout { get; set; }
        public int ExerciseID { get; set; }
        public Exercise Exercise { get; set; }
    }
}
