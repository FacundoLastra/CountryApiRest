using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [JsonIgnore]
        public Country MyCountry { get; set; }

        public List<City> Cities { get; set; }
    }
}
