using FlightApp.Domain.AirplaneTypes;
using FlightApp.Domain.Airports;
using FlightApp.Domain.Flights;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Persistence
{
    public partial class FlightAppDbContext 
        : IdentityDbContext
    {

        public FlightAppDbContext(DbContextOptions<FlightAppDbContext> options) 
            : base(options) { }

        public FlightAppDbContext() { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<AirplaneType> AirplaneTypes { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
