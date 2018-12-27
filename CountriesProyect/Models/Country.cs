using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Models
{
    public class Country
    {
        public Country()
        {
            this.states = new List<State>();
        }

        public int id { get; set; }

        [StringLength(30, MinimumLength = 3)][Required(ErrorMessage = "The name of the country is required")]
        public string Name { get; set; }

        public List<State> states { get; set; }
    }
}
