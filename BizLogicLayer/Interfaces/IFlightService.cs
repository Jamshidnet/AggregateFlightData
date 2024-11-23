using SharedModel.Models;

namespace BizLogicLayer.Services
{

    public interface IFlightService
    {
        Task<List<Flight>> SearchFlightsAsync(FlightSearchCriteria criteria);
        Task<bool> BookFlightAsync(BookingRequest request);
    }
}
