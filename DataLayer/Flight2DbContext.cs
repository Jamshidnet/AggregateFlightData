using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SharedModel.Models;

namespace DataLayer
{
    public class Flight2DbContext : DbContext
    {
        public Flight2DbContext(DbContextOptions<Flight2DbContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<BookingRequest> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            
            modelBuilder.Entity<Flight>()
        .Property(f => f.DepartureTime)
        .HasConversion(
            v => v.ToUniversalTime(),  // Convert to UTC before storing
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Flight>()
        .Property(f => f.ArrivalTime)
        .HasConversion(
            v => v.ToUniversalTime(),  // Convert to UTC before storing
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }


        public static void SeedData(MigrationBuilder migrationBuilder)
        {
            var random = new Random();

            for (int i = 0; i < 1000; i++)  // Adjust the number of rows you want to insert
            {
                migrationBuilder.InsertData(
                    table: "Flights",
                    columns: new[] { "FlightNumber", "Airline", "Departure", "Destination", "DepartureTime", "ArrivalTime", "Price", "Layovers", "IsBooked" },
                    values: new object[]
                    {
                    $"FL{random.Next(100, 999)}-{random.Next(1000, 9999)}",  // Flight number
                    new[] { "American Airlines", "Delta", "United", "JetBlue", "Southwest Airlines" }[random.Next(5)],  // Airline
                    new[] { "New York", "Los Angeles", "Chicago", "London", "Paris", "Tokyo", "Berlin" }[random.Next(7)],  // Departure City
                    new[] { "New York", "Los Angeles", "Chicago", "London", "Paris", "Tokyo", "Berlin" }[random.Next(7)],  // Destination City
                    DateTime.Now.AddDays(random.Next(0, 365)).ToUniversalTime(),  // Random Departure Time in the next year
                    DateTime.Now.AddDays(random.Next(1, 365)).ToUniversalTime(),  // Random Arrival Time
                    random.Next(50, 1500) + random.NextDouble(),  // Random Price
                    random.Next(0, 3),  // Random Layovers (0 to 2)
                    random.NextDouble() > 0.5  // Random IsBooked (true or false)
                    });
            }
        }
    }
}
