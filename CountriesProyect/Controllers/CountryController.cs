using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesProyect.Models;
using CountriesProyect.Repositorys;
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
        private readonly ApplicationDbContext context;

        public CountryController (ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Country> Get()
        {
            return context.country.ToList();
        }

        [HttpGet("{id}", Name = "countryById")]
        public IActionResult GetById(int id)
        {
            var country = this.context.country.Include(x => x.states).FirstOrDefault(x => x.id == id);

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
                context.country.Add(country);
                context.SaveChanges();
                return new CreatedAtRouteResult("countryById", new { id = country.id }, country );
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult updateCountry([FromBody] Country country
            , int id)
        {
            if(country.id != id)
            {
                return BadRequest();
            }

            this.context.Entry(country).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            var country = this.context.country.FirstOrDefault(x => x.id == id);

            if(country == null)
            {
                return NotFound();
            }

            this.context.country.Remove(country);
            context.SaveChanges();
            return Ok(country);
        }

    }
}