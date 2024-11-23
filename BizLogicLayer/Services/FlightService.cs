using SharedModel.Models;
using System.Net.Http.Json;

namespace BizLogicLayer.Services
{
    public class FlightService  : IFlightService
    {
        private readonly HttpClient _httpClient;

        public FlightService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Flight>> SearchFlightsAsync(FlightSearchCriteria criteria)
        {
            var response = await _httpClient.PostAsJsonAsync("api/flights/search", criteria);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Flight>>();
        }

        public async Task<bool> BookFlightAsync(BookingRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/flights/book", request);
            return response.IsSuccessStatusCode;
        }
    }

}
