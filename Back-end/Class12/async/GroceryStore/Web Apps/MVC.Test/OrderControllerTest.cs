using AutoMapper;
using AutoMapper.Configuration;
using Controllers;
using Models.ViewModels;
using Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MVC.Model.Redis;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MVC.Test
{
    public class OrderControllerTest : BaseControllerTest
    {
        private readonly Mock<ILogger<OrderController>> loggerMock;
        private readonly Mock<IOrderService> orderServiceMock;
        private readonly Mock<IUserRedisRepository> userRedisRepositoryMock;

        public OrderControllerTest() :base()
        {
            loggerMock = new Mock<ILogger<OrderController>>();
            orderServiceMock = new Mock<IOrderService>();
            userRedisRepositoryMock = new Mock<IUserRedisRepository>();

            //var mappings = new MapperConfigurationExpression();
            //mappings.AddProfile<MappingProfile>();
            //Mapper.Initialize(mappings);
        }

        [Fact]
        public async Task History_Ok()
        {
            //arrange
            appUserParserMock
                .Setup(a => a.Parse(It.IsAny<IPrincipal>()))
                .Returns(new ApplicationUser())
                .Verifiable();

            string customerId = "123";
            List<OrderItemDTO> items = new List<OrderItemDTO> {
                new OrderItemDTO("001", "product 001", 1, 12.34m)
            };
            OrderDTO order = new OrderDTO(items, "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");
            orderServiceMock
                .Setup(c => c.GetAsync(It.IsAny<string>()))
                .ReturnsAsync( new List<OrderDTO> { order })
                .Verifiable();

            var controller = new OrderController(appUserParserMock.Object
                , orderServiceMock.Object
                , loggerMock.Object
                , userRedisRepositoryMock.Object);
            SetControllerUser(customerId, controller);

            //act
            ActionResult actionResult = await controller.History(customerId);

            //assert
            ViewResult viewResult = Assert.IsAssignableFrom<ViewResult>(actionResult);
            List<OrderDTO>  orders = Assert.IsType<List<OrderDTO>>(viewResult.Model);
            Assert.Collection(orders[0].Items,
                i => Assert.Equal("001", i.ProductCode));
            orderServiceMock.Verify();
        }
    }
}
