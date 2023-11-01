using CountryService.Model;

namespace CountryService.Interfaces
{
    public interface ICountryRepository
    {
        void AddCountry(Country country);
        Country GetCountry(int id);
        IEnumerable<Country> GetAll();
        void RemoveCountry(Country country);
        void UpdateCountry(Country country);
        public bool ExistsCountry(int id);
    }
}
