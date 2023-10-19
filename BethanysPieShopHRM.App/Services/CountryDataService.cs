using BethanysPieShopHRM.App.Helpers;
using BethanysPieShopHRM.Shared.Domain;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Text.Json;

namespace BethanysPieShopHRM.App.Services
{
    public class CountryDataService : ICountryDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public CountryDataService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            try
            {
                var results = new List<Country>();
                results = await JsonSerializer.DeserializeAsync<List<Country>>(await _httpClient.GetStreamAsync($"api/country"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<Country>();
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Country> GetCountry(int countryId)
        {
            try
            {
                var results = await JsonSerializer.DeserializeAsync<Country>
                    ( await _httpClient.GetStreamAsync($"api/country/{countryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
