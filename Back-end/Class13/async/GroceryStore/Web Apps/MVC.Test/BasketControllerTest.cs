using Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.ViewModels;
using Moq;
using MVC.Model.Redis;
using MVC.Models;
using Polly.CircuitBreaker;
using Services;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MVC.Test
{
    public class BasketControllerTest : BaseControllerTest
    {
        private readonly Mock<ILogger<BasketController>> loggerMock;
        private readonly Mock<ICatalogService> catalogServiceMock;
        private readonly Mock<IBasketService> basketServiceMock;
        private readonly Mock<IUserRedisRepository> userRedisRepositoryMock;

        public BasketControllerTest() : base()
        {
            loggerMock = new Mock<ILogger<BasketController>>();
            catalogServiceMock = new Mock<ICatalogService>();
            basketServiceMock = new Mock<IBasketService>();
            userRedisRepositoryMock = new Mock<IUserRedisRepository>();
        }

        #region Index
        [Fact]
        public async Task Index_Success()
        {
            //arrange
            var customerId = "cliente_id";
            var products = GetFakeProducts();
            var testProduct = products[0];
            catalogServiceMock
                .Setup(c => c.GetProduct(testProduct.Code))
                .ReturnsAsync(testProduct)
                .Verifiable();

            var itemBasket = new BasketItem(testProduct.Code, testProduct.Code, testProduct.Name, testProduct.Price, 1, testProduct.ImageURL);
            basketServiceMock
                .Setup(c => c.AddItem(customerId, It.IsAny<BasketItem>()))
                .ReturnsAsync(
                new CustomerBasket(customerId,
                    new List<BasketItem>
                    {
                        itemBasket
                    }))
                .Verifiable();
            var controller = GetBasketController();
            SetControllerUser(customerId, controller);

            //act
            var result = await controller.Index(testProduct.Code);

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CustomerBasket>(viewResult.Model);
            Assert.Equal(model.Items[0].ProductName, products[0].Name);
            catalogServiceMock.Verify();
        }

        [Fact]
        public async Task Index_Without_Product_Success()
        {
            //arrange
            var customerId = "cliente_id";
            var products = GetFakeProducts();
            var testProduct = products[0];

            var itemBasket = new BasketItem(testProduct.Code, testProduct.Code, testProduct.Name, testProduct.Price, 1, testProduct.ImageURL);
            basketServiceMock
                .Setup(c => c.GetBasket(customerId))
                .ReturnsAsync(
                new CustomerBasket(customerId,
                    new List<BasketItem>
                    {
                        itemBasket
                    }))
                .Verifiable();

            var controller = GetBasketController();
            SetControllerUser(customerId, controller);

            //act
            var result = await controller.Index();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CustomerBasket>(viewResult.Model);
            Assert.Equal(model.Items[0].ProductName, products[0].Name);
            basketServiceMock.Verify();
        }

        [Fact]
        public async Task Index_BrokenCircuitException()
        {
            //arrange
            var customerId = "cliente_id";
            var products = GetFakeProducts();
            var testProduct = products[0];
            catalogServiceMock
                .Setup(c => c.GetProduct(It.IsAny<string>()))
                .ThrowsAsync(new BrokenCircuitException())
                .Verifiable();

            var itemBasket = new BasketItem(testProduct.Code, testProduct.Code, testProduct.Name, testProduct.Price, 1, testProduct.ImageURL);
            basketServiceMock
                .Setup(c => c.AddItem(customerId, It.IsAny<BasketItem>()))
                .ReturnsAsync(
                new CustomerBasket(customerId,
                    new List<BasketItem>
                    {
                        itemBasket
                    }))
                .Verifiable();

            var controller = GetBasketController();
            SetControllerUser(customerId, controller);

            //act
            var result = await controller.Index(testProduct.Code);

            ////assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //loggerMock.Verify(l => l.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.Once);
            //Assert.True(!string.IsNullOrWhiteSpace(controller.ViewBag.MsgServiceUnavailable as string));
            //catalogServiceMock.Verify();
        }

        [Fact]
        public async Task Index_ProductNotFound()
        {
            //arrange
            var customerId = "cliente_id";
            var products = GetFakeProducts();
            var testProduct = products[0];
            catalogServiceMock
                .Setup(c => c.GetProduct(testProduct.Code))
                .ReturnsAsync((Product)null)
                .Verifiable();

            var controller = GetBasketController();
            SetControllerUser(customerId, controller);

            //act
            var result = await controller.Index(testProduct.Code);

            //assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ProductNotFound", redirectToActionResult.ActionName);
            Assert.Equal("Basket", redirectToActionResult.ControllerName);
            Assert.Equal(redirectToActionResult.Fragment, testProduct.Code);
            catalogServiceMock.Verify();
        }
        #endregion

        #region UpdateQuantidade
        [Fact]
        public async Task UpdateQuantidade_Success()
        {
            //arrange
            var customerId = "cliente_id";
            var controller = GetBasketController();
            SetControllerUser(customerId, controller);
            var itemBasket = GetFakeItemBasket();
            UpdateQuantityInput updateQuantidadeInput = new UpdateQuantityInput("001", 7);
            basketServiceMock
                .Setup(c => c.UpdateItem(customerId, It.IsAny<UpdateQuantityInput>()))
                .ReturnsAsync(new UpdateQuantityOutput(itemBasket, new CustomerBasket()))
                .Verifiable();

            //act
            var result = await controller.UpdateQuantity(updateQuantidadeInput);

            //assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<UpdateQuantityOutput>(okObjectResult.Value);
            catalogServiceMock.Verify();

        }

        [Fact]
        public async Task UpdateQuantidade_Invalid_ProductId()
        {
            //arrange
            var customerId = "cliente_id";
            UpdateQuantityInput updateQuantidadeInput = new UpdateQuantityInput(null, 7);
            basketServiceMock
                .Setup(c => c.UpdateItem(customerId, It.IsAny<UpdateQuantityInput>()))
                .ReturnsAsync(new UpdateQuantityOutput(new BasketItem(), new CustomerBasket()))
                .Verifiable();

            var controller = GetBasketController();
            SetControllerUser(customerId, controller);
            controller.ModelState.AddModelError("ProductId", "Required");

            //act
            var result = await controller.UpdateQuantity(updateQuantidadeInput);

            //assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestObjectResult.Value);
            catalogServiceMock.Verify();

        }

        [Fact]
        public async Task UpdateQuantidade_ProductId_NotFound()
        {
            //arrange
            var customerId = "cliente_id";
            UpdateQuantityInput updateQuantidadeInput = new UpdateQuantityInput("001", 7);
            basketServiceMock
                .Setup(c => c.UpdateItem(customerId, It.IsAny<UpdateQuantityInput>()))
                .ReturnsAsync((UpdateQuantityOutput)null)
                .Verifiable();

            var controller = GetBasketController();
            SetControllerUser(customerId, controller);

            //act
            var result = await controller.UpdateQuantity(updateQuantidadeInput);

            //assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(updateQuantidadeInput, notFoundObjectResult.Value);
            catalogServiceMock.Verify();

        }
        #endregion

        #region Checkout (POST)
        [Fact]
        public async Task Checkout_success()
        {
            //arrange
            var basket = GetBasketController();

            //act
            IActionResult actionResult = await basket.Checkout(new Registration());

            //assert
            ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);
        }

        [Fact]
        public async Task Checkout_Invalid_Registration()
        {
            //arrange
            var basket = GetBasketController();
            basket.ModelState.AddModelError("Email", "Required");

            //act
            IActionResult actionResult = await basket.Checkout(new Registration());

            //assert
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            redirectToActionResult.ControllerName = "BasketController";
            redirectToActionResult.ActionName = "Checkout";
        }

        [Fact]
        public async Task Checkout_Service_Error()
        {
            //arrange
            basketServiceMock
                .Setup(c => c.Checkout(It.IsAny<string>(), It.IsAny<RegistrationViewModel>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            var controller = GetBasketController();

            //act
            IActionResult actionResult = await controller.Checkout(new Registration());

            ////assert
            //ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);
            //loggerMock.Verify(l => l.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.Once);
            //Assert.True(!string.IsNullOrWhiteSpace(controller.ViewBag.MsgServiceUnavailable as string));
            //catalogServiceMock.Verify();

        }

        [Fact]
        public async Task Checkout_Service_BrokenCircuitException()
        {
            //arrange
            basketServiceMock
                .Setup(c => c.Checkout(It.IsAny<string>(), It.IsAny<RegistrationViewModel>()))
                .ThrowsAsync(new BrokenCircuitException())
                .Verifiable();
            var controller = GetBasketController();

            //act
            IActionResult actionResult = await controller.Checkout(new Registration());

            ////assert
            //ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);
            //loggerMock.Verify(l => l.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.Once);
            //Assert.True(!string.IsNullOrWhiteSpace(controller.ViewBag.MsgServiceUnavailable as string));
            //catalogServiceMock.Verify();

        }
        #endregion

        #region Checkout (GET)
        [Fact]
        public async Task Checkout_Index_Get_Success()
        {
            //arrange
            appUserParserMock
                .Setup(a => a.Parse(It.IsAny<IPrincipal>()))
                .Returns(new ApplicationUser())
                .Verifiable();

            var controller = GetBasketController();
            SetControllerUser("001", controller);
            //act
            IActionResult actionResult = await controller.Checkout();

            //assert
            Assert.IsType<ViewResult>(actionResult);
            appUserParserMock.Verify();
        }

        [Fact]
        public async Task Checkout_Index_Get_Error()
        {
            //arrange
            appUserParserMock
                .Setup(a => a.Parse(It.IsAny<IPrincipal>()))
                .Returns((ApplicationUser)null)
                .Verifiable();

            var controller = GetBasketController();
            SetControllerUser("001", controller);

            //act
            IActionResult actionResult = await controller.Checkout();

            ////assert
            //Assert.IsType<ViewResult>(actionResult);
            //loggerMock.Verify(l => l.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()), Times.Once);
            //appUserParserMock.Verify();

        }
        #endregion

        private BasketController GetBasketController()
        {
            return new BasketController(contextAccessorMock.Object, appUserParserMock.Object, loggerMock.Object, catalogServiceMock.Object, basketServiceMock.Object, userRedisRepositoryMock.Object);
        }
    }
}
