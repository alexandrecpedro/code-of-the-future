using Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using MVC.Model.Redis;
using Polly.CircuitBreaker;
using Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MVC.Test
{
    public class CatalogControllerTest : BaseControllerTest
    {
        private readonly Mock<ICatalogService> catalogServiceMock;
        private readonly Mock<ILogger<CatalogController>> loggerMock;
        private readonly Mock<IUserRedisRepository> userRedisRepositoryMock;

        public CatalogControllerTest() : base()
        {
            catalogServiceMock = new Mock<ICatalogService>();
            loggerMock = new Mock<ILogger<CatalogController>>();
            userRedisRepositoryMock = new Mock<IUserRedisRepository>(); ;
    }

    [Fact]
        public async Task Index_sucesso()
        {
            //arrange
            IList<Product> fakeProducts = GetFakeProducts();
            catalogServiceMock
                .Setup(s => s.GetProducts())
                .ReturnsAsync(fakeProducts)
               .Verifiable();

            var catalogController = 
                new CatalogController(catalogServiceMock.Object, loggerMock.Object, userRedisRepositoryMock.Object);

            //act
            var resultado = await catalogController.Index();

            //assert
            var viewResult = Assert.IsType<ViewResult>(resultado);
            var model = Assert.IsAssignableFrom<IList<Product>>(viewResult.ViewData.Model);

            Assert.Collection(model,
                               item => Assert.Equal(fakeProducts[0].Code, item.Code),
                               item => Assert.Equal(fakeProducts[1].Code, item.Code),
                               item => Assert.Equal(fakeProducts[2].Code, item.Code)
                );
            catalogServiceMock.Verify();
        }

        [Fact]
        public async Task Index_BrokenCircuitException()
        {
            //arrange
            catalogServiceMock
                .Setup(s => s.GetProducts())
                .ThrowsAsync(new BrokenCircuitException());

            //act
            var catalogController =
                new CatalogController(catalogServiceMock.Object, loggerMock.Object, userRedisRepositoryMock.Object);

            var result = await catalogController.Index();
            var model = result as IList<Product>;

            //assert
            Assert.Null(model);
            Assert.True(!string.IsNullOrWhiteSpace(catalogController.ViewBag.MsgServiceUnavailable));
        }

        [Fact]
        public async Task Index_Exception()
        {
            //arrange
            catalogServiceMock
                .Setup(s => s.GetProducts())
                .ThrowsAsync(new Exception());

            //act
            var catalogController =
                new CatalogController(catalogServiceMock.Object, loggerMock.Object, userRedisRepositoryMock.Object);

            var result = await catalogController.Index();
            var model = result as IList<Product>;

            //assert
            Assert.Null(model);
            Assert.True(!string.IsNullOrWhiteSpace(catalogController.ViewBag.MsgServiceUnavailable));
        }
    }
}
