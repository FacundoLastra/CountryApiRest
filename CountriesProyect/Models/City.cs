using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CountriesProyect.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Poblation { get; set; }
        [ForeignKey("State")]
        public int StateID { get; set; }
        [JsonIgnore]
        public State State { get; set; }
    }
}
