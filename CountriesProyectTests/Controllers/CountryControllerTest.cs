using CountriesProyect.Controllers;
using CountriesProyect.Models;
using CountriesProyect.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CountriesProyectTests.Controllers
{
    public class CountryControllerTest
    {
        [Fact]
        public void Controller_Get_All_Countries()
        {
            
            var serviceMock = new Mock<ICountriesService>();
            serviceMock.Setup(x => x.getAllCountries()).Returns(() => new List<Country>
            {
                new Country{ Id= 0, Name = "Argentina" },
                new Country{ Id= 1, Name = "Peru"}
            });
            var controller = new CountryController(serviceMock.Object);

            var result = controller.Get();            

            Assert.Equal(2, ((List<Country>)result).Count);

        }

        [Fact]
        public void GoodWay_Controller_Get_By_Id()
        {
            var serviceMock = new Mock<ICountriesService>();
            serviceMock.Setup(x => x.getCountryById(1))
                .Returns(() => new Country { Id = 1, Name = "Argentina" });

            var controller = new CountryController(serviceMock.Object);

            IActionResult result = controller.GetById(1);

            Assert.IsType<OkObjectResult>((ActionResult)result);    
          }

        [Fact]
        public void BadWay_Controller_Get_By_Id()
        {
            var serviceMock = new Mock<ICountriesService>();
            serviceMock.Setup(x => x.getCountryById(1))
                .Returns(() => new Country { Id = 1, Name = "Argentina" });

            var controller = new CountryController(serviceMock.Object);

            IActionResult result = controller.GetById(3);

            Assert.IsType<NotFoundResult>((ActionResult)result);
        }

        [Fact]
        public void GoodWay_Controller_create_Country()
        {
            var countryToSave = new Country { Id = 1, Name = "Argentina" };

            var serviceMock = new Mock<ICountriesService>();

            var controller = new CountryController(serviceMock.Object);

            IActionResult result = controller.createCountry(countryToSave);

            Assert.IsType<CreatedAtRouteResult>((ActionResult)result);
        }

        [Fact]
        public void GoodWay_Controller_Delete_Country()
        {
            var serviceMock = new Mock<ICountriesService>();
            serviceMock.Setup(x => x.deleteById(1))
                .Returns(() => true);
            
            var controller = new CountryController(serviceMock.Object);

            IActionResult result = controller.delete(1);

            Assert.IsType<OkObjectResult>((ActionResult)result);
        }

        [Fact]
        public void BadWay_Controller_Delete_Country()
        {
            var serviceMock = new Mock<ICountriesService>();
            serviceMock.Setup(x => x.deleteById(1))
                .Returns(() => false);

            var controller = new CountryController(serviceMock.Object);

            IActionResult result = controller.delete(1);

            Assert.IsType<NotFoundResult>((ActionResult)result);
        }

        [Fact]
        public void GoodWay_Controller_Update_Country()
        {
            var countryToUpdate = new Country { Id = 1, Name = "Argentina" };
            var serviceMock = new Mock<ICountriesService>();          

            var controller = new CountryController(serviceMock.Object);

            IActionResult result = controller.updateCountry(countryToUpdate, countryToUpdate.Id);

            Assert.IsType<OkResult>((ActionResult)result);

        }

        [Fact]
        public void BadWay_Controller_Update_Country()
        {
            var countryToUpdate = new Country { Id = 1, Name = "Argentina" };
            var serviceMock = new Mock<ICountriesService>();

            var controller = new CountryController(serviceMock.Object);

            IActionResult result = controller.updateCountry(countryToUpdate, 3);

            Assert.IsType<BadRequestResult>((ActionResult)result);

        }
    }
}
