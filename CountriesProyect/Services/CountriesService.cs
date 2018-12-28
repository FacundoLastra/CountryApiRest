using CountriesProyect.Models;
using CountriesProyect.Repositorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ApplicationDbContext context;

        public CountriesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Country> getAllCountries()
        {
            return context.country.Include(x=> x.States).ThenInclude(x=> x.Cities).ToList();

        }

        public void addCountry(Country country)
        {
            this.context.country.Add(country);
            this.context.SaveChanges();
        }

        public Country getCountryById(int id)
        {
            return this.context.country.FirstOrDefault(x => x.Id == id);  
        }

        public Boolean deleteById(int id)
        {
            var country = this.getCountryById(id);
            if(country == null)
            {
                return false;
            }            
            this.context.country.Remove(country);
            this.context.SaveChanges();            
            return true;
        }

        public void updateCountry( Country country)
        {
            this.context.Entry(country).State = EntityState.Modified;
            this.context.SaveChanges();
        }


    }
}
