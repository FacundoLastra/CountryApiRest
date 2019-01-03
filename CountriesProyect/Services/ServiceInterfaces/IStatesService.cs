using CountriesProyect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Services
{
    public interface IStatesService
    {
        List<State> GetAllStates(int countryId);
        void AddState(State state);
        State GetStateById(int id);
        Boolean DeleteById(int id);
        void UpdateState(State state);
    }
}
