using FlightApp.Domain.Flights;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Infrastructure.Persistence
{
    internal class FlightAppDbContext 
        : DbContext
    {

        public FlightAppDbContext(DbContextOptions<FlightAppDbContext> options) 
            : base(options) { }

        public FlightAppDbContext() { }

        public DbSet<Flight> Flights { get; set; }
    }
}
