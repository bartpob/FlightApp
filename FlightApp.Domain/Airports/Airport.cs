using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Domain.Airports
{
    public class Airport
    {
        public Airport() { }

        private Airport(string iata, string city, string country) 
        {
            IATA = iata;
            City = city;
            Country = country;
        }
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string IATA { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public static Airport Create(string iata, string city, string country)
        {
            return new Airport(iata, city, country);
        }
    }
}
