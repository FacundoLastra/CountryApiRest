using CountriesProyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Services
{
    public interface ICitiesService
    {
        List<City> getAllCities();
        void addCity(City city);
        City getCityById(int id);
        Boolean deleteById(int id);
        void updateCity(City city);
    }
}
