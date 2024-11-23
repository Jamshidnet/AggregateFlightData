using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeFixForSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Call the SeedData method here during migration to insert random data
            FlightDbContext.SeedData(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete the seeded data when rolling back the migration
            migrationBuilder.Sql("DELETE FROM Flights");
        }
    }
}
