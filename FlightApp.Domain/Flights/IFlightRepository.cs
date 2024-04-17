using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Domain.Flights
{
    public interface IFlightRepository
    {
        public Task CreateAsync(Flight flight);
        public Task<List<Flight>> GetAllAsync();
        public Task<Flight> GetByIdAsync(Guid id);
        public Task UpdateAsync(Flight flight);
        public Task DeleteAsync(Guid Id);
    }
}
