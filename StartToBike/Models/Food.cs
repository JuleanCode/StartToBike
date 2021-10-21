using System;
using System.Collections.Generic;

namespace StartToBike.Models
{
    public class Food
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }

        public virtual ICollection<Diet> Diets { get; set; }
    }
}