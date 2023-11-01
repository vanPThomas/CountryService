using CountryService.Exceptions;
using CountryService.Interfaces;
using CountryService.Model;

namespace CountryService.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private Dictionary<int, Country> _data = new Dictionary<int, Country>();

        public CountryRepository()
        {
            _data.Add(1, new Country(1, "Belgie", "Brussel", "Europa"));
            _data.Add(2, new Country(2, "Peru", "Lima", "Zuid Amerika"));
            _data.Add(3, new Country(3, "Duitsland", "Berlijn", "Europa"));
            _data.Add(4, new Country(4, "Zweden", "Stockholm", "Europa"));
            _data.Add(5, new Country(5, "Noorwegen", "Oslo", "Europa"));
        }

        public IEnumerable<Country> GetAll()
        {
            return _data.Values;
        }

        public Country GetCountry(int id)
        {
            if (_data.ContainsKey(id))
                return _data[id];
            else
                throw new CountryException("Country Does Not Exists!");
        }

        public void AddCountry(Country country)
        {
            if (!_data.ContainsKey(country.Id))
                _data.Add(country.Id, country);
            else
                throw new CountryException("country already exists");
        }

        public void RemoveCountry(Country country)
        {
            if (_data.ContainsKey(country.Id))
                _data.Remove(country.Id);
            else
                throw new CountryException("Counrry Doesn't Exist");
        }

        public void UpdateCountry(Country country)
        {
            if (_data.ContainsKey(country.Id))
                _data[country.Id] = country;
            else
                throw new CountryException("Country Doesn't Exist");
        }

        public bool ExistsCountry(int id)
        {
            if (_data.ContainsKey(id))
                return true;
            else
                return false;
        }
    }
}
