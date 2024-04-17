using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Domain.Flights
{
    public class Flight
    {
        public Flight() { }
        private Flight(string flightNumber, DateTime flightDate, 
            string departure, string destination, string airplaneType)
        {
            FlightNumber = flightNumber;
            FlightDate = flightDate;
            Departure = departure;
            Destination = destination;
            AirplaneType = airplaneType;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string FlightNumber { get; private set; }
        public DateTime FlightDate { get; private set; } 
        public string Departure { get; private set; }
        public string Destination { get; private set; }
        public string AirplaneType { get; private set; }

        public static Flight Create(string flightNumber, DateTime flightDate, 
            string departure, string destination, string airplaneType)
        {
            return new Flight(flightNumber, flightDate, departure, destination, airplaneType);
        }

        public void Update(string flightNumber, DateTime flightDate,
            string departure, string destination, string airplaneType)
        {
            FlightNumber = flightNumber;
            FlightDate = flightDate;
            Departure = departure;
            Destination = destination;
            AirplaneType = airplaneType;
        }

    }
}
