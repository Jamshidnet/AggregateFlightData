using Microsoft.EntityFrameworkCore;
using SharedModel.Models;

namespace DataLayer
{
    public class Flight2DbContext : DbContext
    {
        public Flight2DbContext(DbContextOptions<Flight2DbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<BookingRequest> Bookings { get; set; }
    }
}
