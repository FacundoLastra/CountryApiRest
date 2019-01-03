using CountriesProyect.Models;
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
   public class StatesServiceTest
    {
        [Fact]
        public void Add_State_To_Database()
        {
            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_state_to_database")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new StatesService(context);
                service.AddState(stateToSave);
            }
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Single(context.states.ToList());
                Assert.Equal("Buenos Aires", context.states.FirstOrDefault().Name);
            }
        }

        [Fact]
        public void Get_All_States_In_Database()
        {
            var country = new Country();
            country.Name = "Argentina";
            country.Id = 1;

            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";
            stateToSave.MyCountry = country;
            stateToSave.CountryId = country.Id;

            var otherStateToSave = new State();
            otherStateToSave.Name = "Tucuman";
            otherStateToSave.MyCountry = country;
            otherStateToSave.CountryId = country.Id;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_state_in_database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new StatesService(context);
                service.AddState(stateToSave);
                service.AddState(otherStateToSave);
            }
            using (var context = new ApplicationDbContext(options))
            {
                var service = new StatesService(context);
                Assert.Equal(2, service.GetAllStates(1).Count);
            }
        }

        [Fact]
        public void Get_State_By_Id()
        {
            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";
            stateToSave.Id = 1;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_state_in_database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new StatesService(context);
                service.AddState(stateToSave);
                Assert.NotNull(service.GetStateById(1));
            }        
        }

        [Fact]
        public void Delete_State_By_Id()
        {

            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";
            stateToSave.Id = 1;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_All_state_in_database")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new StatesService(context);
                service.AddState(stateToSave);
                Assert.NotNull(service.GetStateById(1));
            }
            using (var context = new ApplicationDbContext(options))
            {
                var service = new StatesService(context);
                service.DeleteById(1);
                Assert.Empty(context.states.ToList());
            }
        }

        [Fact]
        public void Update_State()
        {
            var stateToSave = new State();
            stateToSave.Name = "Buenos Aires";
            stateToSave.Id = 1;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_State")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new StatesService(context);
                service.AddState(stateToSave);

                var stateSaved = service.GetStateById(1);
                stateSaved.Name = "updatedName";
                service.UpdateState(stateSaved);

                Assert.Equal("updatedName", service.GetStateById(1).Name);
            }
        }
    }
}
