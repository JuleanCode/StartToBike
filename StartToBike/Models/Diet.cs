using System;
using System.Collections.Generic;

namespace StartToBike.Models
{
    public class Diet
    {
        public int ID { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Food> Foods { get; set; }
    }
}