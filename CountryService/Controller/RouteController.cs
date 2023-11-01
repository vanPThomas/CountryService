using CountryService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CountryService.Controller
{
    [ApiController]
    public class RouteController : ControllerBase
    {
        private ICountryRepository _countryRepository;

        public RouteController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }
    }
}
