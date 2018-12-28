using CountriesProyect.Models;
using CountriesProyect.Repositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Services
{
    public class CitiesService :ICitiesService
    {
        private readonly ApplicationDbContext context;

        public CitiesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void addCity(City city)
        {
            this.context.cities.Add(city);
            this.context.SaveChanges();           
        }

        public bool deleteById(int id)
        {
            var city = this.getCityById(id);

            if( city == null)
            {
                return false;
            }

            this.context.cities.Remove(city);
            this.context.SaveChanges();
            return true;
        }

        public List<City> getAllCities()
        {
            return this.context.cities.ToList();
        }

        public City getCityById(int id)
        {
            return this.context.cities.FirstOrDefault(x => x.Id == id);
        }

        public void updateCity(City city)
        {
            this.context.Entry(city).State = EntityState.Modified;
            this.context.SaveChanges();
        }
    }
}
