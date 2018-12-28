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
        public int id { get; set; }
        public string name { get; set; }
        public long poblation { get; set; }
        [ForeignKey("State")]
        public int stateID { get; set; }
        [JsonIgnore]
        public State state { get; set; }
    }
}
