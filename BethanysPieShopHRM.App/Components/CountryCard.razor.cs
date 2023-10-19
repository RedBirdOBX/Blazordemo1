using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BethanysPieShopHRM.App.Components
{
    public partial class CountryCard
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        public List<Country> Countries { get; set; } = new List<Country>();

        protected async override Task OnInitializedAsync()
        {
            var url = "https://localhost:7039/api/country";
            var countries = await HttpClient.GetFromJsonAsync<List<Country>>(url);
            Countries = countries.ToList();
        }
    }
}
