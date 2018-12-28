using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesProyect.Models;
using CountriesProyect.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace CountriesProyect.Services
{
    public class StatesService : IStatesService
    {
        private readonly ApplicationDbContext context;

        public StatesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void addState(State state)
        {
            this.context.states.Add(state);
            this.context.SaveChanges();
        }

        public bool deleteById(int id)
        {
            var state = this.getStateById(id);
            if(state == null)
            {
                return false;
            }

            this.context.states.Remove(state);
            this.context.SaveChanges();
            return true;
        }

        public List<State> getAllStates(int countryId)
        {
            return context.states.Where(x => x.CountryId == countryId).Include(x => x.Cities).ToList();
        }

        public State getStateById(int id)
        {
            return this.context.states.FirstOrDefault(x => x.Id == id);
        }

        public void updateState(State state)
        {
            this.context.Entry(state).State = EntityState.Modified;
            this.context.SaveChanges();
        }
    }
}
