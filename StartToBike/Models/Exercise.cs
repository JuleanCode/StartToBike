using System;
using System.Collections.Generic;

namespace StartToBike.Models
{
    public class Exercise
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}