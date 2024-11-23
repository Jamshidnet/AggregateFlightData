using SharedModel.Models;

namespace BizLogicLayer.Services
{
    using DataLayer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using System.Threading;

    public interface IFlightAggregatorService
    {
        Task<IEnumerable<Flight>> GetAggregatedFlightsAsync(FlightSearchCriteria criteria);
    }
}
