using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Domain.Airports
{
    public interface IAirportRepository
    {
        public Task<Airport?> GetByIataAsync(string iata);
    }
}
