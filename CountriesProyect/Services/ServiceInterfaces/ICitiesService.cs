using CountriesProyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Services
{
    public interface ICitiesService
    {
        List<City> GetAllCities();
        void AddCity(City city);
        City GetCityById(int id);
        Boolean DeleteById(int id);
        void UpdateCity(City city);
    }
}
