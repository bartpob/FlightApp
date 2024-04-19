using FlightApp.Domain.Airports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Persistence.Repositories
{
    internal class AirportRepository(FlightAppDbContext _dbContext)
        : IAirportRepository
    {
        public async Task<Airport?> GetByIataAsync(string iata)
        {
            return await _dbContext.Airports.FirstOrDefaultAsync(a => a.IATA == iata);
        }
    }
}
