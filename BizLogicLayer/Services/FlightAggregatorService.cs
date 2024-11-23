using SharedModel.Models;

namespace BizLogicLayer.Services
{
    using DataLayer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using System.Threading;

    public class FlightAggregatorService : IFlightAggregatorService
    {
        private readonly IMemoryCache _cache;
        private readonly FlightDbContext _flightDbContext;
        private readonly Flight2DbContext _secondFlightDbContext;
        private const int SourceTimeoutMilliseconds = 2500; // 2.5 seconds timeout

        public FlightAggregatorService(IMemoryCache cache, FlightDbContext flightDbContext, Flight2DbContext secondFlightDbContext)
        {
            _cache = cache;
            _flightDbContext = flightDbContext;
            _secondFlightDbContext = secondFlightDbContext;
        }

        public async Task<IEnumerable<Flight>> GetAggregatedFlightsAsync(FlightSearchCriteria criteria)
        {
            var cacheKey = $"Search_{criteria.Departure}_{criteria.Destination}_{criteria.DepartureDate?.ToString("yyyyMMdd")}_{criteria.MaxPrice}_{criteria.MaxLayovers}_{criteria.Airline}";

            // Try to get cached data
            if (_cache.TryGetValue(cacheKey, out List<Flight> cachedFlights))
            {
                return cachedFlights;
            }

            // Fetch from sources with timeout handling
            var source1Task = _flightDbContext.Flights.ToListAsync();
            var source2Task = _secondFlightDbContext.Flights.ToListAsync();

            var tasks = new[] { source1Task, source2Task };

            var allFlights = new List<Flight>();
            foreach (var task in tasks)
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(SourceTimeoutMilliseconds));
                if (completedTask == task)
                {
                    allFlights.AddRange(await task); // Task completed within timeout
                }
            }

            // Apply filtering
            if (!string.IsNullOrEmpty(criteria.Departure))
                allFlights = allFlights.Where(f => f.Departure == criteria.Departure).ToList();

            if (!string.IsNullOrEmpty(criteria.Destination))
                allFlights = allFlights.Where(f => f.Destination == criteria.Destination).ToList();

            if (criteria.DepartureDate.HasValue)
                allFlights = allFlights.Where(f => f.DepartureTime.Date == criteria.DepartureDate.Value.Date).ToList();

            if (criteria.MaxPrice.HasValue)
                allFlights = allFlights.Where(f => f.Price <= criteria.MaxPrice.Value).ToList();

            if (criteria.MaxLayovers.HasValue)
                allFlights = allFlights.Where(f => f.Layovers <= criteria.MaxLayovers.Value).ToList();

            if (!string.IsNullOrEmpty(criteria.Airline))
                allFlights = allFlights.Where(f => f.Airline == criteria.Airline).ToList();

            // Cache the result
            _cache.Set(cacheKey, allFlights, TimeSpan.FromMinutes(10));

            return allFlights;
        }
    }


}
