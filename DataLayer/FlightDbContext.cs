using Microsoft.EntityFrameworkCore;
using SharedModel.Models;

namespace DataLayer
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<BookingRequest> Bookings { get; set; }
    }
}
