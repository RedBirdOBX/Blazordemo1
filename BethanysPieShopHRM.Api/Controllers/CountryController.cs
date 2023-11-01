using BethanysPieShopHRM.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BethanysPieShopHRM.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IConfiguration _configuration;

        public CountryController(ICountryRepository countryRepository, IConfiguration configuration)
        {
            _countryRepository = countryRepository;
            _configuration = configuration;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetCountries()
        {
            // this works....
            //var answer = _configuration["Secret"];

            return Ok(_countryRepository.GetAllCountries());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            return Ok(_countryRepository.GetCountryById(id));
        }
    }
}
