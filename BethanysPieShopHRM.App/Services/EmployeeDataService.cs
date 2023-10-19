using BethanysPieShopHRM.App.Helpers;
using BethanysPieShopHRM.Shared.Domain;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace BethanysPieShopHRM.App.Services
{
    public class EmployeeDataService : IEmployeeDataService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        public EmployeeDataService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }


        public async Task<Employee> AddEmployee(Employee employee)
        {
            var json = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/employee", json);

            if (response.IsSuccessStatusCode)
            {
                // clear the cache
                await _localStorageService.ClearAsync();

                return await JsonSerializer.DeserializeAsync<Employee>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var json = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            // clear the cache
            await _localStorageService.ClearAsync();

            await _httpClient.PutAsync("api/employee", json);
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await _httpClient.DeleteAsync($"api/employee/{employeeId}");
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                var results = new List<Employee>();
                bool empListKeyExists = await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeesListKey);

                if (empListKeyExists)
                {
                    bool emplListExpiredKeyExisits = await _localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeeListExpirationKey);

                    // do we have employees in local storage?
                    var employees = await _localStorageService.GetItemAsync<List<Employee>>(LocalStorageConstants.EmployeesListKey);
                    if (employees.Any())
                    {
                        if (emplListExpiredKeyExisits)
                        {
                            // try parse here
                            var expValue = await _localStorageService.GetItemAsync<string>(LocalStorageConstants.EmployeeListExpirationKey);
                            DateTime.TryParse(expValue, out var expDate);

                            if (expDate > DateTime.Now)
                            {
                                results = employees;
                                return results;
                            }
                        }
                    }
                }

                // else, call API for data and set local storage
                results = await GetDataFromAPI();
                await SetLocalStorage(results);
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task SetLocalStorage(List<Employee> employees)
        {
            // store in local storage
            await _localStorageService.SetItemAsync(LocalStorageConstants.EmployeesListKey, employees);

            // set expiration
            await _localStorageService.SetItemAsync(LocalStorageConstants.EmployeeListExpirationKey, DateTime.Now.AddMinutes(5));
        }

        private async Task<List<Employee>> GetDataFromAPI()
        {
            try
            {
                var results = new List<Employee>();
                results = await JsonSerializer.DeserializeAsync<List<Employee>>(await _httpClient.GetStreamAsync($"api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? new List<Employee>();
                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Employee> GetEmployeeDetails(int employeeId)
        {
            try
            {
                // why is this throwing error?

                var results2 = await JsonSerializer.DeserializeAsync<Employee>
                    ( await _httpClient.GetStreamAsync($"api/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                var stream = await _httpClient.GetStreamAsync($"api/employee/{employeeId}");

                var results = await JsonSerializer.DeserializeAsync<Employee>
                    ( await _httpClient.GetStreamAsync($"api/employee/{employeeId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return results;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
