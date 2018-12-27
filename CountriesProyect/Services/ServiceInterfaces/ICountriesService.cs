using CountriesProyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Services
{
    public interface ICountriesService
    {
        List<Country> getAllCountries();
        void addCountry(Country country);
        Country getCountryById(int id);
        Boolean deleteById(int id);
        void updateCountry(Country country);
    }
}
