﻿using System;
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
        public IEnumerable GetAllCities()
        {
            return this.citiesService.GetAllCities();
        }

        [HttpGet("{id}", Name = "cityById")]
        public IActionResult GetById(int id)
        {
            var city = this.citiesService.GetCityById(id);
            if(city == null)
            {
                return NotFound(city);
            }
            return Ok(city);
        }

        [HttpPost]
        public IActionResult AddCity([FromBody]City city)
        {
            if (ModelState.IsValid)
            {
                this.citiesService.AddCity(city);
                return new CreatedAtRouteResult("cityById", new { id = city.Id }, city);
            }
            return BadRequest();     
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCity (int id)
        {
            bool deleted = this.citiesService.DeleteById(id);
            if (deleted)
            {
                return Ok(deleted);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCity ([FromBody] City city, int id)
        {
            if(id != city.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            this.citiesService.UpdateCity(city);
            return Ok(city);
        }

    }
}