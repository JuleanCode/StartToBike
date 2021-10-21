using System;
using System.Collections.Generic;

namespace StartToBike.Models
{
    public class Weight
    {
        public int ID { get; set; }
        public int Content { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}