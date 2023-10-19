using BethanysPieShopHRM.App.Helpers;
using BethanysPieShopHRM.Shared.Domain;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Text.Json;

namespace BethanysPieShopHRM.App.Services
{
    public class JobCategoryDataService : IJobCategoryDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public JobCategoryDataService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<IEnumerable<JobCategory>> GetAllJobCategories()
        {
            try
            {
                var results = new List<JobCategory>();
                results = await JsonSerializer.DeserializeAsync<List<JobCategory>>(await _httpClient.GetStreamAsync($"api/jobcategory"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<JobCategory>();
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<JobCategory> GetJobCategory(int jobCategoryId)
        {
            try
            {
                var results = await JsonSerializer.DeserializeAsync<JobCategory>
                    ( await _httpClient.GetStreamAsync($"api/jobcategory/{jobCategoryId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
