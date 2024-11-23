using SharedModel.Models;

namespace BizLogicLayer.Services
{
    public static class MockFlightSource
    {
        public static async Task<List<Flight>> GetFlightsFromSource1Async()
        {
            await Task.Delay(2000); // Simulates a delay of 2 seconds
            return new List<Flight>
        {
            new Flight { FlightNumber = "S1-101", Airline = "AirlineA", Departure = "JFK", Destination = "LAX", DepartureTime = DateTime.UtcNow.AddHours(3), ArrivalTime = DateTime.UtcNow.AddHours(6), Price = 300, Layovers = 0 },
            new Flight { FlightNumber = "S1-102", Airline = "AirlineB", Departure = "JFK", Destination = "LAX", DepartureTime = DateTime.UtcNow.AddHours(4), ArrivalTime = DateTime.UtcNow.AddHours(8), Price = 350, Layovers = 1 }
        };
        }

        public static async Task<List<Flight>> GetFlightsFromSource2Async()
        {
            await Task.Delay(3000); // Simulates a delay of 3 seconds
            return new List<Flight>
        {
            new Flight { FlightNumber = "S2-201", Airline = "AirlineA", Departure = "JFK", Destination = "LAX", DepartureTime = DateTime.UtcNow.AddHours(2), ArrivalTime = DateTime.UtcNow.AddHours(5), Price = 280, Layovers = 0 },
            new Flight { FlightNumber = "S2-202", Airline = "AirlineC", Departure = "JFK", Destination = "LAX", DepartureTime = DateTime.UtcNow.AddHours(6), ArrivalTime = DateTime.UtcNow.AddHours(9), Price = 320, Layovers = 1 }
        };
        }
    }

}
