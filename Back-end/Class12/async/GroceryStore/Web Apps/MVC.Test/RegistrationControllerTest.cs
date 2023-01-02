using Controllers;
using Models;
using Models.ViewModels;
using Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MVC.Model.Redis;
using MVC.SignalR;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MVC.Test
{
    public class RegistrationControllerTest : BaseControllerTest
    {
        private readonly Mock<ILogger<RegistrationController>> loggerMock;
        private readonly Mock<IUserRedisRepository> userRedisRepositoryMock;

        public RegistrationControllerTest() : base()
        {
            loggerMock = new Mock<ILogger<RegistrationController>>();
            userRedisRepositoryMock = new Mock<IUserRedisRepository>();
        }

        #region Index
        [Fact]
        public async Task Index_success()
        {
            //arrange
            appUserParserMock
                .Setup(aup => aup.Parse(It.IsAny<ClaimsPrincipal>()))
                .Returns(new ApplicationUser
                {
                    District = "bbb",
                    ZipCode = "ccc",
                    AdditionalAddress = "ccc",
                    Email = "eee",
                    Address = "eee",
                    City = "mmm",
                    Name = "nnn",
                    Phone = "ttt",
                    State = "uuu"
                })
               .Verifiable();

            //act
            var registrationController = 
                new RegistrationController(appUserParserMock.Object, loggerMock.Object, userRedisRepositoryMock.Object);
            registrationController.ControllerContext.HttpContext = contextMock.Object;
            var result = await registrationController.Index();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<RegistrationViewModel>(viewResult.ViewData.Model);
            Assert.Equal<RegistrationViewModel>(model,
                new RegistrationViewModel
                {
                    District = "bbb",
                    ZipCode = "ccc",
                    AdditionalAddress = "ccc",
                    Email = "eee",
                    Address = "eee",
                    City = "mmm",
                    Name = "nnn",
                    Phone = "ttt",
                    State = "uuu"
                });
            appUserParserMock.Verify();
        }

        [Fact]
        public async Task Index_No_User()
        {
            //arrange
            appUserParserMock
                .Setup(a => a.Parse(It.IsAny<IPrincipal>()))
                .Returns((ApplicationUser)null)
               .Verifiable();

            var controller =
                new RegistrationController(appUserParserMock.Object, loggerMock.Object, userRedisRepositoryMock.Object);

            SetControllerUser("001", controller);

            //act
            IActionResult result = await controller.Index();

            ////assert
            //Assert.IsType<ViewResult>(result);
            //loggerMock.Verify(l => l.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.Once);
            //appUserParserMock.Verify();

        }
        #endregion
    }
}
