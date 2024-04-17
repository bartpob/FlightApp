using FlightApp.Domain.Flights;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Persistence.Repositories
{
    internal class FlightRepository(FlightAppDbContext _dbContext)
        : IFlightRepository
    {
        public async Task CreateAsync(Flight flight)
        {
            await _dbContext.Flights.AddAsync(flight);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            var flightToRemove = await GetByIdAsync(Id);

            _dbContext.Flights.Remove(flightToRemove);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Flight>> GetAllAsync()
        {
            return await _dbContext.Flights.ToListAsync();
        }

        public async Task<Flight> GetByIdAsync(Guid id)
        {
            return await _dbContext.Flights.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task UpdateAsync(Flight flight)
        {
            _dbContext.Entry(flight).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
    }
}
