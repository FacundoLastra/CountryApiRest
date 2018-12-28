using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesProyect.Models;
using CountriesProyect.Repositorys;
using CountriesProyect.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CountriesProyect.Controllers
{
    [Route("api/Country")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CountryController : ControllerBase
    {
        private readonly ICountriesService service;

        public CountryController(ICountriesService service) => this.service = service;

        [HttpGet]
        public IEnumerable<Country> Get()
        {
            return this.service.getAllCountries();
        }

        [HttpGet("{id}", Name = "countryById")]
        public IActionResult GetById(int id)
        {
            var country = this.service.getCountryById(id);

            if(country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPost]
        public IActionResult createCountry([FromBody] Country country)
        {
            if (ModelState.IsValid)
            {
                this.service.addCountry(country);
                return new CreatedAtRouteResult("countryById", new { id = country.Id }, country );
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult updateCountry([FromBody] Country country
            , int id)
        {
            if (country.Id != id)
            {
                return BadRequest();
            }

            this.service.updateCountry(country);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            Boolean deleted = this.service.deleteById(id);

            if(deleted == false)
            {
                return NotFound();
            }            
            return Ok(deleted);
        }

    }
}