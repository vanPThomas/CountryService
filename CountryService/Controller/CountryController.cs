using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using CountryService.Exceptions;
using CountryService.Interfaces;
using CountryService.Model;

namespace CountryService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpHead]
        [HttpGet]
        public IEnumerable<Country> GetAll()
        {
            return _countryRepository.GetAll();
        }

        [HttpHead("{id}")]
        [HttpGet("{id}")]
        public ActionResult<Country> Get(int id)
        {
            try
            {
                return _countryRepository.GetCountry(id);
            }
            catch (CountryException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Country> Post([FromBody] Country country)
        {
            _countryRepository.AddCountry(country);
            return CreatedAtAction(nameof(Get), new { id = country.Id }, country);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!_countryRepository.ExistsCountry(id))
            {
                return NotFound();
            }
            _countryRepository.RemoveCountry(_countryRepository.GetCountry(id));
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Country country)
        {
            if (country == null || country.Id != id)
            {
                return BadRequest();
            }
            if (!_countryRepository.ExistsCountry(id))
            {
                _countryRepository.AddCountry(country);
                return CreatedAtAction(nameof(Get), new { id = country.Id }, country);
            }
            var countryDB = _countryRepository.GetCountry(id);
            _countryRepository.UpdateCountry(country);
            return new NoContentResult();
        }

        [HttpPatch("{id}")]
        public ActionResult<Country> Patch(
            int id,
            [FromBody] JsonPatchDocument<Country> countryPatch
        )
        {
            var countryDB = _countryRepository.GetCountry(id);
            if (countryDB == null)
            {
                return NotFound();
            }
            try
            {
                countryPatch.ApplyTo(countryDB);
                return countryDB;
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("[Action]/{id:int=3}")]
        [HttpGet]
        public IActionResult Start(int id)
        {
            return Ok(_countryRepository.GetCountry(id));
        }
    }
}
