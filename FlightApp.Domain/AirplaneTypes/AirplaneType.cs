using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Domain.AirplaneTypes
{
    public class AirplaneType
    {
        public AirplaneType() { }

        private AirplaneType(string airplane)
        {
            Airplane = airplane;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Airplane { get; private set; }

        public static AirplaneType Create(string airplane)
        {
            return new AirplaneType(airplane);
        }
    }
}
