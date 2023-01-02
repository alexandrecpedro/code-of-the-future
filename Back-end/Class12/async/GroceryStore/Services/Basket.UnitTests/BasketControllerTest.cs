using Basket.API.Controllers;
using Basket.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Rebus.Bus;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using IBasketIdentityService = Basket.API.Services.IIdentityService;

namespace Basket.API.Tests
{
    public class BasketControllerTest
    {
        private readonly Mock<IBasketRepository> _basketRepositoryMock;
        private readonly Mock<IBasketIdentityService> _identityServiceMock;
        private readonly Mock<IBus> _serviceBusMock;
        private readonly Mock<ILogger<BasketController>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;

        public BasketControllerTest()
        {
            _basketRepositoryMock = new Mock<IBasketRepository>();
            _identityServiceMock = new Mock<IBasketIdentityService>();
            _serviceBusMock = new Mock<IBus>();
            _loggerMock = new Mock<ILogger<BasketController>>();
            _configurationMock = new Mock<IConfiguration>();
        }

        #region Get

        [Fact]
        public async Task Get_basket_cliente_sucesso()
        {
            //Arrange
            var fakeCustomerId = "1";
            var basketFake = GetCustomerBasketFake(fakeCustomerId);

            _basketRepositoryMock
                .Setup(x => x.GetBasketAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(basketFake))
                .Verifiable();
            _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeCustomerId);
            _serviceBusMock.Setup(x => x.Publish(It.IsAny<Messages.Events.CheckoutEvent>(), null));

            //Act
            var basketController = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);

            var actionResult = await basketController.Get(fakeCustomerId) as OkObjectResult;

            //Assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            CustomerBasket customerBasket = Assert.IsAssignableFrom<CustomerBasket>(okObjectResult.Value);
            Assert.Equal(fakeCustomerId, customerBasket.CustomerId);
            Assert.Equal(basketFake.Items[0].ProductId, customerBasket.Items[0].ProductId);
            Assert.Equal(basketFake.Items[1].ProductId, customerBasket.Items[1].ProductId);
            Assert.Equal(basketFake.Items[2].ProductId, customerBasket.Items[2].ProductId);
            _basketRepositoryMock.Verify();
            _identityServiceMock.Verify();
            _serviceBusMock.Verify();
        }

        [Fact]
        public async Task Get_basket_cliente_no_client()
        {
            //arrange
            var controller =
                new BasketController(_basketRepositoryMock.Object,
                _identityServiceMock.Object, _serviceBusMock.Object,
                _loggerMock.Object, _configurationMock.Object);

            //act
            IActionResult actionResult = await controller.Get(null);

            //assert
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.IsType<SerializableError>(badRequestObjectResult.Value);
        }

        [Fact]
        public async Task Get_basket_cliente_basket_not_found()
        {
            //arrange
            string customerId = "123";
            CustomerBasket basketFake = GetCustomerBasketFake(customerId);
            _basketRepositoryMock
                .Setup(r => r.GetBasketAsync(customerId))
                .ReturnsAsync((CustomerBasket)null)
                .Verifiable();

            var controller =
                new BasketController(_basketRepositoryMock.Object,
                _identityServiceMock.Object, _serviceBusMock.Object,
                _loggerMock.Object, _configurationMock.Object);

            //act
            IActionResult actionResult = await controller.Get(customerId);

            //assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            CustomerBasket customerBasket = Assert.IsAssignableFrom<CustomerBasket>(okObjectResult.Value);
            Assert.Equal(customerId, customerBasket.CustomerId);
            _basketRepositoryMock.Verify();
        }
        #endregion

        #region Post
        [Fact]
        public async Task Post_basket_cliente_sucesso()
        {
            //Arrange
            var fakeCustomerId = "1";
            var fakeCustomerBasket = GetCustomerBasketFake(fakeCustomerId);

            _basketRepositoryMock.Setup(x => x.UpdateBasketAsync(It.IsAny<CustomerBasket>()))
                .Returns(Task.FromResult((CustomerBasket)fakeCustomerBasket))
                .Verifiable();
            _serviceBusMock.Setup(x => x.Publish(It.IsAny<Messages.Events.CheckoutEvent>(), null))
                .Verifiable();

            //Act
            var basketController = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);

            var actionResult = await basketController.Post(fakeCustomerBasket) as OkObjectResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.Equal(((CustomerBasket)actionResult.Value).CustomerId, fakeCustomerId);

            _basketRepositoryMock.Verify();
        }

        [Fact]
        public async Task Post_basket_cliente_not_found()
        {
            //Arrange
            var fakeCustomerId = "1";
            var fakeCustomerBasket = GetCustomerBasketFake(fakeCustomerId);

            _basketRepositoryMock.Setup(x => x.UpdateBasketAsync(It.IsAny<CustomerBasket>()))
                .ThrowsAsync(new KeyNotFoundException())
                .Verifiable();
            _identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeCustomerId);
            _serviceBusMock.Setup(x => x.Publish(It.IsAny<Messages.Events.CheckoutEvent>(), null));

            //Act
            var basketController = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);

            var actionResult = await basketController.Post(fakeCustomerBasket);

            //Assert
            NotFoundResult notFoundResult = Assert.IsType<NotFoundResult>(actionResult);
            _basketRepositoryMock.Verify();
            _identityServiceMock.Verify();
            _serviceBusMock.Verify();
        }

        [Fact]
        public async Task Post_basket_cliente_invalid_model()
        {
            //Arrange
            var controller = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);
            controller.ModelState.AddModelError("CustomerId", "Required");

            //Act
            var actionResult = await controller.Post(new CustomerBasket());

            //Assert
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        #endregion

        #region Checkout
        [Fact]
        public async Task Fazer_Checkout_Sem_Cesta_Deve_Retornar_BadRequest()
        {
            //Arrange
            var fakeCustomerId = "2";

            var basketController = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);
            RegistrationViewModel input = new RegistrationViewModel();
            basketController.ModelState.AddModelError("Email", "Required");

            //Act
            ActionResult<bool> actionResult = await basketController.Checkout(fakeCustomerId, input);

            //Assert
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.IsAssignableFrom<SerializableError>(badRequestObjectResult.Value);
        }

        [Fact]
        public async Task Fazer_Checkout_Basket_Not_Found()
        {
            //Arrange
            var fakeCustomerId = "123";
            _basketRepositoryMock.Setup(x => x.GetBasketAsync(It.IsAny<string>()))
                .ThrowsAsync(new KeyNotFoundException());
            var basketController = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);
            RegistrationViewModel input = new RegistrationViewModel();

            //Act
            ActionResult<bool> actionResult = await basketController.Checkout(fakeCustomerId, input);

            //Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }


        [Fact]
        public async Task Fazer_Checkout_Com_Basket_Deveria_Publicar_CheckoutEvent()
        {
            //arrange
            var fakeCustomerId = "1";
            var fakeCustomerBasket = GetCustomerBasketFake(fakeCustomerId);

            _basketRepositoryMock.Setup(x => x.GetBasketAsync(It.IsAny<string>()))
                 .Returns(Task.FromResult(fakeCustomerBasket))
                .Verifiable();

            //_identityServiceMock.Setup(x => x.GetUserIdentity()).Returns(fakeCustomerId)
            //    .Verifiable();

            var basketController = new BasketController(
                _basketRepositoryMock.Object, _identityServiceMock.Object, _serviceBusMock.Object,
                _loggerMock.Object, _configurationMock.Object);

            basketController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(
                        new ClaimsIdentity(new Claim[] { new Claim("unique_name", "testuser") }))
                }
            };

            //Act
            ActionResult<bool> actionResult = await basketController.Checkout(fakeCustomerId, new RegistrationViewModel());

            //assert
            _serviceBusMock.Verify(mock => mock.Publish(It.IsAny<Messages.Events.CheckoutEvent>(), null), Times.Once);
            Assert.NotNull(actionResult);
            _basketRepositoryMock.Verify();
            //_identityServiceMock.Verify();
        }

        #endregion

        #region AddItem
        [Fact]
        public async Task AddItem_success()
        {
            //arrange
            var customerId = "123";
            var basket = GetCustomerBasketFake(customerId);
            BasketItem input = new BasketItem("004", "004", "produto 004", 45.67m, 4);
            var items = basket.Items;
            items.Add(input);
            _basketRepositoryMock
                .Setup(c => c.AddBasketAsync(customerId, It.IsAny<BasketItem>()))
                .ReturnsAsync(new CustomerBasket
                {
                    CustomerId = customerId,
                    Items = items
                })
                .Verifiable();

            var controller = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);

            //act
            ActionResult<CustomerBasket> actionResult = await controller.AddItem(customerId, input);

            //assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            CustomerBasket customerBasket = Assert.IsAssignableFrom<CustomerBasket>(okObjectResult.Value);
            Assert.Equal(4, customerBasket.Items.Count());
            _basketRepositoryMock.Verify();
            _identityServiceMock.Verify();
            _serviceBusMock.Verify();
        }

        [Fact]
        public async Task AddItem_basket_notfound()
        {
            //arrange
            var customerId = "123";
            BasketItem input = new BasketItem("004", "004", "produto 004", 45.67m, 4);
            _basketRepositoryMock
                .Setup(c => c.AddBasketAsync(customerId, It.IsAny<BasketItem>()))
                .ThrowsAsync(new KeyNotFoundException());
            var controller = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);

            //act
            ActionResult<CustomerBasket> actionResult = await controller.AddItem(customerId, input);

            //assert
            NotFoundObjectResult notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal(customerId, notFoundObjectResult.Value);
        }

        [Fact]
        public async Task AddItem_basket_invalid_model()
        {
            //arrange
            var customerId = "123";
            BasketItem input = new BasketItem("004", "004", "produto 004", 45.67m, 4);
            _basketRepositoryMock
                .Setup(c => c.AddBasketAsync(customerId, It.IsAny<BasketItem>()))
                .ThrowsAsync(new KeyNotFoundException());
            var controller = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);
            controller.ModelState.AddModelError("ProductId", "Required");

            //act
            ActionResult<CustomerBasket> actionResult = await controller.AddItem(customerId, input);

            //assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }
        #endregion

        #region UpdateItem
        [Fact]
        public async Task UpdateItem_success()
        {
            //arrange
            var customerId = "123";
            var basket = GetCustomerBasketFake(customerId);
            BasketItem input = new BasketItem("004", "004", "produto 004", 45.67m, 4);
            var items = basket.Items;
            items.Add(input);
            _basketRepositoryMock
                .Setup(c => c.UpdateBasketAsync(customerId, It.IsAny<UpdateQuantityInput>()))
                .ReturnsAsync(new UpdateQuantityOutput(input,
                new CustomerBasket
                {
                    CustomerId = customerId,
                    Items = items
                }))
                .Verifiable();

            var controller = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);

            //act
            ActionResult<UpdateQuantityOutput> actionResult = await controller.UpdateItem(customerId, new UpdateQuantityInput(input.ProductId, input.Quantity));

            //assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            UpdateQuantityOutput updateQuantidadeOutput = Assert.IsAssignableFrom<UpdateQuantityOutput>(okObjectResult.Value);
            Assert.Equal(input.ProductId, updateQuantidadeOutput.BasketItem.ProductId);
            _basketRepositoryMock.Verify();
            _identityServiceMock.Verify();
            _serviceBusMock.Verify();
        }

        [Fact]
        public async Task UpdateItem_basket_notfound()
        {
            //arrange
            var customerId = "123";
            BasketItem input = new BasketItem("004", "004", "produto 004", 45.67m, 4);
            _basketRepositoryMock
                .Setup(c => c.UpdateBasketAsync(customerId, It.IsAny<UpdateQuantityInput>()))
                .ThrowsAsync(new KeyNotFoundException());
            var controller = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);

            //act
            ActionResult<UpdateQuantityOutput> actionResult = await controller.UpdateItem(customerId, new UpdateQuantityInput(input.ProductId, input.Quantity));

            //assert
            NotFoundObjectResult notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal(customerId, notFoundObjectResult.Value);
        }

        [Fact]
        public async Task UpdateItem_basket_invalid_model()
        {
            //arrange
            var customerId = "123";
            BasketItem input = new BasketItem("004", "004", "produto 004", 45.67m, 4);
            var controller = new BasketController(
                _basketRepositoryMock.Object,
                _identityServiceMock.Object,
                _serviceBusMock.Object,
                _loggerMock.Object,
                _configurationMock.Object);
            controller.ModelState.AddModelError("ProductId", "Required");

            //act
            ActionResult<UpdateQuantityOutput> actionResult = await controller.UpdateItem(customerId, new UpdateQuantityInput(input.ProductId, input.Quantity));

            //assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }
        #endregion

        private CustomerBasket GetCustomerBasketFake(string fakeCustomerId)
        {
            return new CustomerBasket(fakeCustomerId)
            {
                CustomerId = fakeCustomerId,
                Items = new List<BasketItem>()
                {
                    new BasketItem("001", "001", "produto 001", 12.34m, 1),
                    new BasketItem("002", "002", "produto 002", 23.45m, 2),
                    new BasketItem("003", "003", "produto 003", 34.56m, 3)
                }
            };
        }

    }
}
