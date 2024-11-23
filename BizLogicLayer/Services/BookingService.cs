using SharedModel.Models;

namespace BizLogicLayer.Services
{
    public class BookingService
    {
        private List<Flight> _allFlights;

        public BookingService()
        {
        }

        public async Task InitializeAsync()
        {
            _allFlights = (await MockFlightSource.GetFlightsFromSource1Async())
                 .Concat(await MockFlightSource.GetFlightsFromSource2Async())
                 .ToList();
        }

        public bool BookFlight(BookingRequest request)
        {
            //var flight = _allFlights.FirstOrDefault(f => f.FlightNumber == request.FlightNumber);
            //if (flight == null || flight.IsBooked)
            //    return false;

            //flight.IsBooked = true;
            return true;
        }
    }
}
