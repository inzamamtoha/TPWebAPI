using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristPlaceApi.Models
{
    public class Places
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Rating { get; set; }
        public string Picture { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
    }
}
