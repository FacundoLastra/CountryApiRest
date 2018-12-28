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
using CountriesProyect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CountriesProyect.Controllers
{
    [Route("api/Country/{countryId}/State")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StateController : ControllerBase
    {
        private readonly IStatesService stateService;

        public StateController(IStatesService stateService)
        {
            this.stateService = stateService;
        }

        [HttpGet]
        public IEnumerable getAll(int countryId)
        {
            return this.stateService.getAllStates(countryId);
        }

        [HttpGet("{id}", Name = "stateById")]
        public IActionResult getOneState(int id)
        {
            var country = this.stateService.getStateById(id);

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
                this.stateService.addState(state);
                return new CreatedAtRouteResult("stateById", new { id = state.Id }, state);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteState(int id)
        {
            bool deleted = this.stateService.deleteById(id);

            if(deleted == false)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }

        [HttpPut("{id}")]
        public IActionResult updateState(int id, [FromBody] State state)
        {
            if(id != state.Id)
            {
                return BadRequest();
            }
            this.stateService.updateState(state);
            return Ok(state);
        }
    }
}