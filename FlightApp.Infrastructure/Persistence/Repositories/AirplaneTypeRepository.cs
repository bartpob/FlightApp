using FlightApp.Domain.AirplaneTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Persistence.Repositories
{
    internal class AirplaneTypeRepository(FlightAppDbContext _dbContext)
        : IAirplaneTypeRepository
    {
        public async Task<AirplaneType?> GetByAirplaneNameAsync(string airplaneName)
        {
            return await _dbContext.AirplaneTypes.FirstOrDefaultAsync(a => a.Airplane ==  airplaneName);
        }
    }
}
