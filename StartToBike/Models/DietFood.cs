using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartToBike.Models
{
    public class DietFood
    {
        public int DietID { get; set; }
        public Diet Diet { get; set; }
        public int FoodID { get; set; }
        public Food Food { get; set; }
    }
}
