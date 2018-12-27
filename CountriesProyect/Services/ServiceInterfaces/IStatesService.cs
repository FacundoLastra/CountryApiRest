using CountriesProyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Services
{
    public interface IStatesService
    {
        List<State> getAllStates(int countryId);
        void addState(State state);
        State getStateById(int id);
        Boolean deleteById(int id);
        void updateState(State state);
    }
}
