using BethanysPieShopHRM.Api.Models;
using Microsoft.AspNetCore.Mvc;


namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IConfiguration _configuration;

        public CountryController(ICountryRepository countryRepository, IConfiguration configuration)
        {
            _countryRepository = countryRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            // example of pulling from an appSettings using Configuration svc
            // https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-6.0
            var answer = _configuration["Secret"];

            return Ok(_countryRepository.GetAllCountries());
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            return Ok(_countryRepository.GetCountryById(id));
        }
    }
}
