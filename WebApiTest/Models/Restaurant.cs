﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTest.Models
{
    public class Restaurant
    {
        public Restaurant()
        {
            locationRestaurants = new HashSet<LocationRestaurant>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LocationRestaurant> locationRestaurants { get; set; }
    }
}
