using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTest.Models;

namespace WebApiTest.Pagination
{
    public class IndexViewModel
    {
        public IEnumerable<LocationRestaurant> LocationRest { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
