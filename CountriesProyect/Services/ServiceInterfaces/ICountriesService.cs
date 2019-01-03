using CountriesProyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Services
{
    public interface ICountriesService
    {
        List<Country> GetAllCountries();
        void AddCountry(Country country);
        Country GetCountryById(int id);
        Boolean DeleteById(int id);
        void UpdateCountry(Country country);
    }
}
