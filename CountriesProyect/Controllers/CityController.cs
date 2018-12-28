using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesProyect.Models;
using CountriesProyect.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountriesProyect.Controllers
{
    [Route("api/Country/{countryId}/State/{stateId}/City")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class CityController : ControllerBase
    {
        private readonly ICitiesService citiesService;

        public CityController(ICitiesService citiesService)
        {
            this.citiesService = citiesService;
        }

        [HttpGet]
        public IEnumerable getAllCities()
        {
            return this.citiesService.getAllCities();
        }

        [HttpGet("{id}", Name = "cityById")]
        public IActionResult getById(int id)
        {
            var city = this.citiesService.getCityById(id);
            if(city == null)
            {
                return BadRequest(city);
            }
            return Ok(city);
        }

        [HttpPost]
        public IActionResult addCity([FromBody]City city)
        {
            if (ModelState.IsValid)
            {
                this.citiesService.addCity(city);
                return new CreatedAtRouteResult("cityById", new { id = city.Id }, city);
            }
            return BadRequest();     
        }

        [HttpDelete("{id}")]
        public IActionResult deleteCity (int id)
        {
            bool deleted = this.citiesService.deleteById(id);
            if (deleted)
            {
                return Ok(deleted);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult updateCity ([FromBody] City city, int id)
        {
            if(id != city.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            this.citiesService.updateCity(city);
            return Ok(city);
        }

    }
}