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
            return this.stateService.GetAllStates(countryId);
        }

        [HttpGet("{id}", Name = "stateById")]
        public IActionResult getOneState(int id)
        {
            var country = this.stateService.GetStateById(id);

            if(country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [HttpPost]
        public IActionResult addState([FromBody] State state)
        {
            if (ModelState.IsValid)
            {
                this.stateService.AddState(state);
                return new CreatedAtRouteResult("stateById", new { id = state.Id }, state);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteState(int id)
        {
            bool deleted = this.stateService.DeleteById(id);

            if(deleted == false)
            {
                return NotFound();
            }
            return Ok(deleted);
        }

        [HttpPut("{id}")]
        public IActionResult updateState([FromBody] State state, int id)
        {
            if(id != state.Id)
            {
                return BadRequest();
            }
            this.stateService.UpdateState(state);
            return Ok(state);
        }
    }
}