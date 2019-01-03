using CountriesProyect.Controllers;
using CountriesProyect.Models;
using CountriesProyect.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CountriesProyectTests.Controllers
{
   public class CityControllerTest
    {
        [Fact]
        public void Controller_Get_All_Cities()
        {
            var serviceMock = new Mock<ICitiesService>();
            serviceMock.Setup(x => x.getAllCities()).Returns(() => new List<City>
            {
                new City{ Id= 0, Name = "Buenos Aires" },
                new City{ Id= 1, Name = "Mar del Plata"}
            });
            var controller = new CityController(serviceMock.Object);

            var result = controller.getAllCities();

            Assert.Equal(2, ((List<City>)result).Count);
        }

        [Fact]
        public void GoodWay_Controller_Get_By_Id()
        {
            var serviceMock = new Mock<ICitiesService>();
            serviceMock.Setup(x => x.getCityById(1))
                .Returns(() => new City { Id = 1, Name = "Buenos Aires" });

            var controller = new CityController(serviceMock.Object);

            IActionResult result = controller.getById(1);

            Assert.IsType<OkObjectResult>((ActionResult)result);
        }

        [Fact]
        public void BadWay_Controller_Get_By_Id()
        {
            var serviceMock = new Mock<ICitiesService>();
            serviceMock.Setup(x => x.getCityById(1))
                .Returns(() => new City { Id = 1, Name = "Buenos Aires" });

            var controller = new CityController(serviceMock.Object);

            IActionResult result = controller.getById(2);

            Assert.IsType<NotFoundObjectResult>((ActionResult)result);
        }

        [Fact]
        public void GoodWay_Controller_create_State()
        {
            var cityToSave = new City { Id = 1, Name = "Mar del Plata" };

            var serviceMock = new Mock<ICitiesService>();

            var controller = new CityController(serviceMock.Object);

            IActionResult result = controller.addCity(cityToSave);

            Assert.IsType<CreatedAtRouteResult>((ActionResult)result);
        }

        [Fact]
        public void GoodWay_Controller_Delete_City()
        {
            var serviceMock = new Mock<ICitiesService>();
            serviceMock.Setup(x => x.deleteById(1))
                .Returns(() => true);

            var controller = new CityController(serviceMock.Object);

            IActionResult result = controller.deleteCity(1);

            Assert.IsType<OkObjectResult>((ActionResult)result);
        }

        [Fact]
        public void BadWay_Controller_Delete_City()
        {
            var serviceMock = new Mock<ICitiesService>();
            serviceMock.Setup(x => x.deleteById(1))
                .Returns(() => false);

            var controller = new CityController(serviceMock.Object);

            IActionResult result = controller.deleteCity(1);

            Assert.IsType<NotFoundResult>((ActionResult)result);
        }

        [Fact]
        public void GoodWay_Controller_Update_City()
        {
            var cityToUpdate = new City { Id = 1, Name = "Mar del Plata" };
            var serviceMock = new Mock<ICitiesService>();

            var controller = new CityController(serviceMock.Object);

            IActionResult result = controller.updateCity(cityToUpdate, cityToUpdate.Id);

            Assert.IsType<OkObjectResult>((ActionResult)result);
        }

        [Fact]
        public void BadWay_Controller_Update_City()
        {
            var cityToUpdate = new City { Id = 1, Name = "Mar del Plata" };
            var serviceMock = new Mock<ICitiesService>();

            var controller = new CityController(serviceMock.Object);

            IActionResult result = controller.updateCity(cityToUpdate, 2);

            Assert.IsType<BadRequestResult>((ActionResult)result);
        }
    }
}
