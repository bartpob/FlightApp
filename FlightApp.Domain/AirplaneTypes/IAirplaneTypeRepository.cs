using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Domain.AirplaneTypes
{
    public interface IAirplaneTypeRepository
    {
        public Task<AirplaneType?> GetByAirplaneNameAsync(string airplaneName);
    }
}
