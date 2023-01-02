using Catalog.API.Controllers;
using Catalog.API.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.UnitTests
{
    public class ProductControllerTest
    {
        private readonly Mock<ILogger<ProductController>> loggerMock;
        private readonly Mock<IProductQueries> productQueriesMock;

        public ProductControllerTest()
        {
            this.loggerMock = new Mock<ILogger<ProductController>>();
            this.productQueriesMock = new Mock<IProductQueries>();
        }

        public async Task GetProducts_success()
        {
            //arrange
            var products = new List<Product>();
            var controller = new ProductController(loggerMock.Object, productQueriesMock.Object);

            //act
            var actionResult = await controller.GetProducts();

            //assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            List<Product> catalog = Assert.IsType<List<Product>>(okObjectResult.Value);
            Assert.Collection(catalog,
                item => Assert.Equal(products[0].Code, catalog[0].Code),
                item => Assert.Equal(products[1].Code, catalog[1].Code),
                item => Assert.Equal(products[2].Code, catalog[2].Code)
            );
        }

        [Fact]
        public async Task GetProducts_empty_catalog()
        {
            //arrange
            IList<Product> products = new List<Product>();
            productQueriesMock
                .Setup(q => q.GetProductsAsync(It.IsAny<string>()))
                .ReturnsAsync(products)
               .Verifiable();

            var controller = new ProductController(loggerMock.Object, productQueriesMock.Object);

            //act
            var actionResult = await controller.GetProducts();

            //assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            List<Product> catalog = Assert.IsType<List<Product>>(okObjectResult.Value);
            Assert.Empty(catalog);
            productQueriesMock.Verify();
        }

        [Fact]
        public async Task GetProducts_successAsync2()
        {
            //arrange
            const string productCodigo = "001";
            IList<Product> products = GetFakeProducts();
            productQueriesMock
                .Setup(q => q.GetProductAsync(productCodigo))
                .ReturnsAsync(products[0])
               .Verifiable();

            var controller = new ProductController(loggerMock.Object, productQueriesMock.Object);

            //act
            var actionResult = await controller.GetProducts(productCodigo);

            //assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Product product = Assert.IsType<Product>(okObjectResult.Value);
            Assert.Equal(products[0].Code, product.Code);
            productQueriesMock.Verify();
        }

        [Fact]
        public async Task GetProducts_not_found()
        {
            //arrange
            const string productCodigo = "001";
            var products = GetFakeProducts();
            productQueriesMock
                .Setup(q => q.GetProductAsync(productCodigo))
                .ReturnsAsync((Product)null)
               .Verifiable();

            var controller = new ProductController(loggerMock.Object, productQueriesMock.Object);
            
            //act
            var actionResult = await controller.GetProducts(productCodigo);

            //assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
            productQueriesMock.Verify();
        }

        protected IList<Product> GetFakeProducts()
        {
            return new List<Product>
            {
                new Product("001", "product 001", 12.34m),
                new Product("002", "product 002", 23.45m),
                new Product("003", "product 003", 34.56m)
            };
        }
    }
}
