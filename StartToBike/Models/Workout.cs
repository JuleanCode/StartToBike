using System;
using System.Collections.Generic;

namespace StartToBike.Models
{
    public class Workout
    {
        public int ID { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }
        public virtual List<Exercise> Exercises { get; set; }
        public virtual List<WorkoutExercise> WorkoutExercises { get; set; }
    }
}