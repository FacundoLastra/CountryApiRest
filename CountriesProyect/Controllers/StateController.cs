using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesProyect.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountriesProyect.Repositorys;

namespace CountriesProyect.Controllers
{
    [Route("api/Country/{countryId}/State")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public StateController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable getAll(int countryId)
        {
            return context.states.Where(x => x.countryId == countryId).ToList();
        }

        [HttpGet("{id}", Name = "stateById")]
        public IActionResult getOneState(int id)
        {
            var country = this.context.states.FirstOrDefault(x => x.Id == id);

            if(country == null)
            {
                return BadRequest();
            }

            return Ok(country);
        }

        [HttpPost]
        public IActionResult addState([FromBody] State state)
        {
            if (ModelState.IsValid)
            {
                this.context.Add(state);
                this.context.SaveChanges();
                return new CreatedAtRouteResult("stateById", new { id = state.Id }, state);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteState(int id)
        {
            var state = this.context.states.FirstOrDefault(x => x.Id == id);

            if(state == null)
            {
                return BadRequest();
            }

            this.context.states.Remove(state);
            this.context.SaveChanges();
            return Ok(state);
        }

        [HttpPut("{id}")]
        public IActionResult updateState(int id, [FromBody] State state)
        {
            if(id != state.Id)
            {
                return BadRequest();
            }
            this.context.Entry(state).State = EntityState.Modified;
            this.context.SaveChanges();
            return Ok(state);
        }
    }
}