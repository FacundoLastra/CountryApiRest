using CountriesProyect.Models;
using CountriesProyect.Repositorys;
using CountriesProyect.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;


namespace CountriesProyectTests.Services
{
    public class CountriesServiceTest
    {
        [Fact]
        public void Add_Country_To_Database()
        {
            var countryToSave = new Country();
            countryToSave.Name = "sarasa";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CountriesService(context);
                service.AddCountry(countryToSave);
            }
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Single(context.country.ToList());
                Assert.Equal("sarasa", context.country.FirstOrDefault().Name);
            }
        }

        [Fact]
        public void Get_All_Countries_in_Database()
        {
            var countryToSave = new Country();
            countryToSave.Name = "sarasa";
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_Element_for_Database")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CountriesService(context);
                service.AddCountry(countryToSave);
                Assert.Single(service.GetAllCountries());
            }
        }

        [Fact]
        public void Delete_Country_By_Id()
        {
            var countryToSave = new Country();
            countryToSave.Name = "sarasa";
            countryToSave.Id = 1;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Country_By_Id")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CountriesService(context);
                service.AddCountry(countryToSave);
                Assert.Single(service.GetAllCountries());
                service.DeleteById(1);
                Assert.Empty(service.GetAllCountries());
            }
        }

        [Fact]
        public void Get_Country_By_Id()
        {
            var countryToSave = new Country();
            countryToSave.Name = "sarasa";
            countryToSave.Id = 1;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Country_By_Id")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CountriesService(context);
                service.AddCountry(countryToSave);
                Assert.NotNull(service.GetCountryById(1));

            }
        }

        [Fact]
        public void Update_Country_By_Id()
        {
            var countryToSave = new Country();
            countryToSave.Name = "sarasa";
            countryToSave.Id = 1;
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Update_Country")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new CountriesService(context);
                service.AddCountry(countryToSave);

                var countrySaved = service.GetCountryById(1);
                countrySaved.Name = "updatedName";
                service.UpdateCountry(countrySaved);

                Assert.Equal("updatedName", service.GetCountryById(1).Name);
            }

        }
    }
}
