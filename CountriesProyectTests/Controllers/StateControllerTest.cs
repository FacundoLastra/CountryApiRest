﻿using CountriesProyect.Controllers;
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
    public class StateControllerTest
    {
        [Fact]
        public void Controller_Get_All_States()
        {

            var serviceMock = new Mock<IStatesService>();
            serviceMock.Setup(x => x.GetAllStates(1)).Returns(() => new List<State>
            {
                new State{ Id= 0, Name = "Buenos Aires" },
                new State{ Id= 1, Name = "Tucuman"}
            });
            var controller = new StateController(serviceMock.Object);

            var result = controller.GetAll(1);

            Assert.Equal(2, ((List<State>)result).Count);
        }

        [Fact]
        public void GoodWay_Controller_Get_By_Id()
        {
            var serviceMock = new Mock<IStatesService>();
            serviceMock.Setup(x => x.GetStateById(1))
                .Returns(() => new State { Id = 1, Name = "Buenos Aires" });

            var controller = new StateController(serviceMock.Object);

            IActionResult result = controller.GetOneState(1);

            Assert.IsType<OkObjectResult>((ActionResult)result);
        }

        [Fact]
        public void BadWay_Controller_Get_By_Id()
        {
            var serviceMock = new Mock<IStatesService>();
            serviceMock.Setup(x => x.GetStateById(1))
                .Returns(() => new State { Id = 1, Name = "Buenos Aires" });

            var controller = new StateController(serviceMock.Object);

            IActionResult result = controller.GetOneState(2);

            Assert.IsType<NotFoundResult>((ActionResult)result);
        }

        [Fact]
        public void GoodWay_Controller_create_State()
        {
            var stateToSave = new State { Id = 1, Name = "Argentina" };

            var serviceMock = new Mock<IStatesService>();

            var controller = new StateController(serviceMock.Object);

            IActionResult result = controller.AddState(stateToSave);

            Assert.IsType<CreatedAtRouteResult>((ActionResult)result);
        }

        [Fact]
        public void GoodWay_Controller_Delete_State()
        {
            var serviceMock = new Mock<IStatesService>();
            serviceMock.Setup(x => x.DeleteById(1))
                .Returns(() => true);

            var controller = new StateController(serviceMock.Object);

            IActionResult result = controller.DeleteState(1);

            Assert.IsType<OkObjectResult>((ActionResult)result);
        }

        [Fact]
        public void BadWay_Controller_Delete_State()
        {
            var serviceMock = new Mock<IStatesService>();
            serviceMock.Setup(x => x.DeleteById(1))
                .Returns(() => true);

            var controller = new StateController(serviceMock.Object);

            IActionResult result = controller.DeleteState(2);

            Assert.IsType<NotFoundResult>((ActionResult)result);
        }

        [Fact]
        public void GoodWay_Controller_Update_State()
        {
            var stateToUpdate = new State { Id = 1, Name = "Buenos Aires" };
            var serviceMock = new Mock<IStatesService>();

            var controller = new StateController(serviceMock.Object);

            IActionResult result = controller.UpdateState(stateToUpdate, stateToUpdate.Id);

            Assert.IsType<OkObjectResult>((ActionResult)result);
        }

        [Fact]
        public void BadWay_Controller_Update_State()
        {
            var stateToUpdate = new State { Id = 1, Name = "Buenos Aires" };
            var serviceMock = new Mock<IStatesService>();

            var controller = new StateController(serviceMock.Object);

            IActionResult result = controller.UpdateState(stateToUpdate, 3);

            Assert.IsType<BadRequestResult>((ActionResult)result);
        }


    }
}
