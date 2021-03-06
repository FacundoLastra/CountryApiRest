﻿using CountriesProyect.Models;
using CountriesProyect.Repositorys;
using CountriesProyect.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CountriesProyectTests.Services
{
    public class CitiesServiceTest
    {
        [Fact]
        public void Add_City_To_Database()
        {
            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";

            var cityToSave = new City();
            cityToSave.Name = "Mar del Plata";
            cityToSave.State = stateToSave;
            

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_City_to_database")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CitiesService(context);
                service.AddCity(cityToSave);
            }
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Single(context.cities.ToList());
                Assert.Equal("Mar del Plata", context.cities.FirstOrDefault().Name);
            }
        }

        [Fact]
        public void Get_All_Cities()
        {
            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";

            var cityToSave = new City();
            cityToSave.Name = "Mar del Plata";
            cityToSave.State = stateToSave;

            var otherCityToSave = new City();
            otherCityToSave.Name = "Balcarce";
            otherCityToSave.State = stateToSave;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_Cities_for_database")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CitiesService(context);
                service.AddCity(cityToSave);
                service.AddCity(otherCityToSave);
            }
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CitiesService(context);
                Assert.Equal(2, service.GetAllCities().Count);
            }
        }

        [Fact]
        public void Get_City_By_Id()
        {
            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";

            var cityToSave = new City();
            cityToSave.Name = "Mar del Plata";
            cityToSave.Id = 1;
            cityToSave.State = stateToSave;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "Get_Cities_by_Id")
               .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CitiesService(context);
                service.AddCity(cityToSave);                
            }
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CitiesService(context);
                Assert.NotNull(service.GetCityById(1));
            }
        }

        [Fact]
        public void Delete_City_By_Id()
        {
            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";

            var cityToSave = new City();
            cityToSave.Name = "Mar del Plata";
            cityToSave.Id = 1;
            cityToSave.State = stateToSave;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "Delete_City_by_Id")
               .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CitiesService(context);
                service.AddCity(cityToSave);
            }
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CitiesService(context);
                service.DeleteById(1);
                Assert.Empty(service.GetAllCities());
            }
        }

        [Fact]
        public void Update_City()
        {
            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";

            var cityToSave = new City();
            cityToSave.Name = "Mar del Plata";
            cityToSave.Id = 1;
            cityToSave.State = stateToSave;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "Update_City")
               .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new CitiesService(context);
                service.AddCity(cityToSave);

                var citySaved = service.GetCityById(1);
                citySaved.Name = "updatedName";
                service.UpdateCity(citySaved);

                Assert.Equal("updatedName", service.GetCityById(1).Name);
            }
        }
    }
}
