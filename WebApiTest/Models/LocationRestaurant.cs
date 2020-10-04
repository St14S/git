using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class LocationRestaurant
    {
        public int Id { get; set; }
        public int Id_rs { get; set; }
        public int Id_ct { get; set; }

        public virtual City _city{ get; set; }
        public virtual Restaurant _restaurant { get; set; }
    }
}
