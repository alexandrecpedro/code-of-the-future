using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ordering.API.Models;
using Ordering.Controllers;
using Ordering.Models;
using Ordering.Models.DTOs;
using Ordering.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Ordering.UnitTests
{
    public class OrderingControllerTest
    {
        private readonly Mock<IOrderRepository> orderRepositoryMock;
        private readonly IMapper mapper;

        public OrderingControllerTest()
        {
            orderRepositoryMock = new Mock<IOrderRepository>();
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = mockMapper.CreateMapper();
        }

        [Theory]
        [InlineData("", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678")]
        [InlineData("customerId", "", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678")]
        [InlineData("customerId", "customerName", "", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678")]
        [InlineData("customerId", "customerName", "customer@email.com", "", "address", "additionalAddress", "district", "city", "state", "12345-678")]
        [InlineData("customerId", "customerName", "customer@email.com", "phone", "", "additionalAddress", "district", "city", "state", "12345-678")]
        [InlineData("customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "", "city", "state", "12345-678")]
        [InlineData("customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "", "state", "12345-678")]
        [InlineData("customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "", "12345-678")]
        [InlineData("customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "")]
        public async Task Post_Invalid_Order(string customerId, string customerName, string customerEmail, string customerPhone, string customerAddress, string customerAdditionalAddress, string customerDistrict, string customerCity, string customerState, string customerZipCode)
        {
            //arrange
            List<OrderItem> items = new List<OrderItem> {
                new OrderItem("001", "product 001", 1, 12.34m)
            };
            Order order = new Order(items, customerId, customerName, customerEmail, customerPhone, customerAddress, customerAdditionalAddress, customerDistrict, customerCity, customerState, customerZipCode);
            var controller = new OrderingController(orderRepositoryMock.Object, mapper);
            controller.ModelState.AddModelError("cliente", "Required");
            //act
            IActionResult actionResult = await controller.Post(order);

            //assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task Post_Invalid_Order_No_Items()
        {
            //arrange
            Order order = new Order(new List<OrderItem>(), "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");
            var controller = new OrderingController(orderRepositoryMock.Object, mapper);
            controller.ModelState.AddModelError("cliente", "Required");
            //act
            IActionResult actionResult = await controller.Post(order);

            //assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task Post_Invalid_Order_Items_Null()
        {
            //arrange
            Order order = new Order(null, "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");
            var controller = new OrderingController(orderRepositoryMock.Object, mapper);
            controller.ModelState.AddModelError("cliente", "Required");
            //act
            IActionResult actionResult = await controller.Post(order);

            //assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task Post_Invalid_Order_Success()
        {
            //arrange
            List<OrderItem> items = new List<OrderItem> {
                new OrderItem("001", "product 001", 1, 12.34m)
            };
            Order order = new Order(items, "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");
            order.Id = 123;
            orderRepositoryMock
                .Setup(r => r.CreateOrUpdate(It.IsAny<Order>()))
                .ReturnsAsync(order);
            var controller = new OrderingController(orderRepositoryMock.Object, mapper);
            //act
            IActionResult actionResult = await controller.Post(order);

            //assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Order orderCriado = Assert.IsType<Order>(okObjectResult.Value);
            Assert.Equal(123, orderCriado.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task Get_Invalid_CustomerId(string customerId)
        {
            //arrange
            var controller = new OrderingController(orderRepositoryMock.Object, mapper);
            SetControllerUser(customerId, controller);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await controller.Get(customerId));

        }

        [Fact]
        public async Task Get_Not_Found()
        {
            //arrange
            orderRepositoryMock
                .Setup(r => r.GetOrders(It.IsAny<string>()))
                .ReturnsAsync((List<Order>)null)
                .Verifiable();

            var controller = new OrderingController(orderRepositoryMock.Object, mapper);
            SetControllerUser("xpto", controller);

            //act
            ActionResult result = await controller.Get("xpto");

            //assert
            Assert.IsType<NotFoundObjectResult>(result);

            orderRepositoryMock.Verify();
        }

        [Fact]
        public async Task Get_Ok()
        {
            //arrange
            List<OrderItem> items = new List<OrderItem> {
                new OrderItem("001", "product 001", 1, 12.34m)
            };
            Order order = new Order(items, "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");
            order.Id = 123;

            orderRepositoryMock
                .Setup(r => r.GetOrders(It.IsAny<string>()))
                .ReturnsAsync(new List<Order> { order })
                .Verifiable();

            var controller = new OrderingController(orderRepositoryMock.Object, mapper);
            SetControllerUser("xpto", controller);

            //act
            ActionResult result = await controller.Get("xpto");

            //assert
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var orders = Assert.IsType<List<OrderDTO>>(objectResult.Value);
            Assert.Collection(orders,
                (p) => Assert.Equal("123", p.Id));

            Assert.Collection(orders[0].Items,
                (i) => Assert.Equal("001", i.ProductCode));

            orderRepositoryMock.Verify();
        }

        protected static void SetControllerUser(string customerId, ControllerBase controller)
        {
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] { new Claim("sub", customerId) }
                ));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

    }
}
