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
            this.States = new List<State>();
        }

        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3)][Required(ErrorMessage = "The name of the country is required")]
        public string Name { get; set; }

        public List<State> States { get; set; }
    }
}
