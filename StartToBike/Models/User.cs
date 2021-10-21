using System;
using System.Collections.Generic;

namespace StartToBike.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Sex { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public int WeightGoal { get; set; }
        public int Rol { get; set; }

        public virtual Workout Workout { get; set; }
        public virtual Diet Diet { get; set; }
        public virtual ICollection<Weight> Weigths { get; set; }
    }
}