using Basket.API.Model;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Basket.API.Tests
{
    public class RedisBasketRepositoryTest
    {
        private readonly Mock<ILogger<RedisBasketRepository>> loggerMock;
        private readonly Mock<IConnectionMultiplexer> redisMock;

        public RedisBasketRepositoryTest()
        {
            loggerMock = new Mock<ILogger<RedisBasketRepository>>();
            redisMock = new Mock<IConnectionMultiplexer>();
        }

        #region GetBasketAsync
        [Fact]
        public async Task GetBasketAsync_success()
        {
            //arrange
            var json = @"{
                  ""CustomerId"": ""123"",
                  ""Items"": [{
                  ""Id"": ""001"",
                  ""ProductId"": ""001"",
                  ""ProdutoNome"": ""Produto 001"",
                  ""Quantidade"": 7,
                  ""PrecoUnitario"": 12.34}]
                }";

            string customerId = "123";
            var databaseMock = new Mock<IDatabase>();
            databaseMock
                .Setup(d => d.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync(json)
                .Verifiable();
            redisMock
                .Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(databaseMock.Object)
                .Verifiable();

            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            var customerBasket = await repository.GetBasketAsync(customerId);

            //assert
            Assert.Equal(customerId, customerBasket.CustomerId);
            Assert.Collection(customerBasket.Items,
                item =>
                {
                    Assert.Equal("001", item.ProductId);
                    Assert.Equal(7, item.Quantity);
                });

            databaseMock.Verify();
            redisMock.Verify();
        }

        [Fact]
        public async Task GetBasketAsync_invalid_customerId()
        {
            //arrange
            string customerId = "";
            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act - assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => repository.GetBasketAsync(customerId));
        }

        [Fact]
        public async Task GetBasketAsync_customerId_NotFound()
        {
            //arrange
            var json = @"{
                  ""CustomerId"": ""123"",
                  ""Items"": []
                }";

            string customerId = "123";
            var databaseMock = new Mock<IDatabase>();
            databaseMock
                .Setup(d => d.StringSetAsync(
                        It.IsAny<RedisKey>(),
                        It.IsAny<RedisValue>(),
                        null,
                        When.Always,
                        CommandFlags.None
                    ))
               .ReturnsAsync(true)
               .Verifiable();

            databaseMock.SetupSequence(d => d.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                    .ReturnsAsync("")
                    .ReturnsAsync(json);                 


            redisMock
                .Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(databaseMock.Object)
                .Verifiable();
            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            var customerBasket = await repository.GetBasketAsync(customerId);

            //assert
            Assert.Equal(customerId, customerBasket.CustomerId);
            Assert.Empty(customerBasket.Items);
            databaseMock.Verify();
            redisMock.Verify();
        }
        #endregion

        #region AddBasketAsync
        [Fact]
        public async Task AddBasketAsync_success()
        {
            //arrange
            var json1 = JsonConvert.SerializeObject(new CustomerBasket("123") { Items = new List<BasketItem> { new BasketItem("001", "001", "produto 001", 12.34m, 1) }});
            var json2 = JsonConvert.SerializeObject(new CustomerBasket("123") { Items = new List<BasketItem> { new BasketItem("001", "001", "produto 001", 12.34m, 1), new BasketItem("002", "002", "produto 002", 12.34m, 2) } });

            string customerId = "123";
            var databaseMock = new Mock<IDatabase>();
            databaseMock
                .Setup(d => d.StringSetAsync(
                        It.IsAny<RedisKey>(),
                        It.IsAny<RedisValue>(),
                        null,
                        When.Always,
                        CommandFlags.None
                    ))
               .ReturnsAsync(true);
            databaseMock
                .SetupSequence(d => d.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync("")
                .ReturnsAsync(json1)
                .ReturnsAsync(json2);

            redisMock
                .Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(databaseMock.Object)
                .Verifiable();

            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            BasketItem item = new BasketItem("002", "002", "produto 002", 12.34m, 2);

            //act
            var customerBasket = await repository.AddBasketAsync(customerId, item);

            //assert
            Assert.Equal(customerId, customerBasket.CustomerId);
            Assert.Collection(customerBasket.Items,
                i =>
                {
                    Assert.Equal("001", i.ProductId);
                    Assert.Equal(1, i.Quantity);
                },
                i =>
                {
                    Assert.Equal("002", i.ProductId);
                    Assert.Equal(2, i.Quantity);
                });
            databaseMock.Verify();
            redisMock.Verify();
        }

        [Fact]
        public async Task AddBasketAsync_invalid_item()
        {
            //arrange
            string customerId = "123";
            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => repository.AddBasketAsync(customerId, null));
        }

        [Fact]
        public async Task AddBasketAsync_invalid_item2()
        {
            //arrange
            string customerId = "123";
            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => repository.AddBasketAsync(customerId, new BasketItem() { ProductId = "" }));
        }


        [Fact]
        public async Task AddBasketAsync_negative_qty()
        {
            //arrange
            string customerId = "123";
            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                () => repository.AddBasketAsync(customerId, new BasketItem() { ProductId = "001", Quantity = -1 }));
        }
        #endregion

        #region UpdateBasketAsync
        [Fact]
        public async Task UpdateBasketAsync_success()
        {
            //arrange
            var json1 = JsonConvert.SerializeObject(new CustomerBasket("123") { Items = new List<BasketItem> { new BasketItem("001", "001", "produto 001", 12.34m, 1) } });
            var json2 = JsonConvert.SerializeObject(new CustomerBasket("123") { Items = new List<BasketItem> { new BasketItem("001", "001", "produto 001", 12.34m, 2) } });

            string customerId = "123";
            var databaseMock = new Mock<IDatabase>();
            databaseMock
                .Setup(d => d.StringSetAsync(
                        It.IsAny<RedisKey>(),
                        It.IsAny<RedisValue>(),
                        null,
                        When.Always,
                        CommandFlags.None
                    ))
               .ReturnsAsync(true)
               .Verifiable();
            databaseMock
                .SetupSequence(d => d.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync("")
                .ReturnsAsync(json1)
                .ReturnsAsync(json2);

            redisMock
                .Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(databaseMock.Object)
               .Verifiable();

            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            var item = new UpdateQuantityInput("001", 2);

            //act
            var output = await repository.UpdateBasketAsync(customerId, item);

            //assert
            Assert.Equal(customerId, output.CustomerBasket.CustomerId);
            Assert.Collection(output.CustomerBasket.Items,
                i =>
                {
                    Assert.Equal("001", i.ProductId);
                    Assert.Equal(2, i.Quantity);
                });

            databaseMock.Verify();
            redisMock.Verify();
        }

        [Fact]
        public async Task UpdateBasketAsync_invalid_item()
        {
            //arrange
            string customerId = "123";
            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => repository.UpdateBasketAsync(customerId, null));
        }

        [Fact]
        public async Task UpdateBasketAsync_invalid_item2()
        {
            //arrange
            string customerId = "123";
            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => repository.UpdateBasketAsync(customerId, new UpdateQuantityInput() { ProductId = "" }));
        }


        [Fact]
        public async Task UpdateBasketAsync_negative_qty()
        {
            //arrange
            string customerId = "123";
            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                () => repository.UpdateBasketAsync(customerId, new UpdateQuantityInput() { ProductId = "001", Quantity = -1 }));
        }
        #endregion

        #region DeleteBasketAsync
        [Fact]
        public async Task DeleteBasketAsync_success()
        {
            //arrange
            string customerId = "123";
            var databaseMock = new Mock<IDatabase>();
            databaseMock
                .Setup(d => d.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync(true)
                .Verifiable();
            redisMock
                .Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(databaseMock.Object)
                .Verifiable();
            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            bool result = await repository.DeleteBasketAsync(customerId);

            //assert
            Assert.True(result);
            databaseMock.Verify();
            redisMock.Verify();
        }

        [Fact]
        public async Task DeleteBasketAsync_failure()
        {
            //arrange
            string customerId = "123";
            var databaseMock = new Mock<IDatabase>();
            databaseMock
                .Setup(d => d.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync(false)
               .Verifiable();
            redisMock
                .Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>()))
                .Returns(databaseMock.Object)
               .Verifiable();

            var repository
                = new RedisBasketRepository(loggerMock.Object, redisMock.Object);

            //act
            bool result = await repository.DeleteBasketAsync(customerId);

            //assert
            Assert.False(result);
            databaseMock.Verify();
            redisMock.Verify();
        }
        #endregion
    }
}
