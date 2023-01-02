using Microsoft.EntityFrameworkCore;
using Moq;
using Ordering.Models;
using Ordering.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Ordering.UnitTests
{
    public class OrderRepositoryTest
    {
        private readonly Mock<ApplicationContext> contextoMock;

        public OrderRepositoryTest()
        {
            this.contextoMock = new Mock<ApplicationContext>();
        }

        [Fact]
        public async Task CreateOrUpdate_Order_NullAsync()
        {
            //arrange
            var repo = new OrderRepository(contextoMock.Object);

            //act+assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => repo.CreateOrUpdate(null));
        }

        [Fact]
        public async Task CreateOrUpdate_No_Items()
        {
            //arrange
            var order = new Order();
            var repo = new OrderRepository(contextoMock.Object);

            //act+assert
            await Assert.ThrowsAsync<NoItemsException>(() => repo.CreateOrUpdate(order));
        }

        [Theory]
        [InlineData("", "product 001", 1, 12.34)]
        [InlineData("001", "", 1, 12.34)]
        [InlineData("001", "product 001", -1, 12.34)]
        [InlineData("001", "product 001", 0, 12.34)]
        [InlineData("001", "product 001", 1, 0)]
        [InlineData("001", "product 001", 1, -20)]
        public async Task CreateOrUpdate_Invalid_Item(string code, string name, int qty, decimal price)
        {
            //arrange
            var order = new Order(
                new List<OrderItem> {
                    new OrderItem("001", "product 001", 1, 12.34m),
                    new OrderItem(code, name, qty, price)
                },
                "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");

            var repo = new OrderRepository(contextoMock.Object);

            //act+assert
            await Assert.ThrowsAsync<InvalidItemException>(() => repo.CreateOrUpdate(order));
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
        public async Task CreateOrUpdate_Invalid_Client_Data(string customerId, string customerName, string customerEmail, string customerPhone, string customerAddress, string customerAdditionalAddress, string customerDistrict, string customerCity, string customerState, string customerZipCode)
        {
            //arrange
            var order = new Order(
                new List<OrderItem> {
                    new OrderItem("001", "product 001", 1, 12.34m),
                    new OrderItem("002", "product 002", 2, 23.45m)
                },
                customerId, customerName, customerEmail, customerPhone, customerAddress, customerAdditionalAddress, customerDistrict, customerCity, customerState, customerZipCode);

            var repo = new OrderRepository(contextoMock.Object);

            //act+assert
            await Assert.ThrowsAsync<InvalidUserDataException>(() => repo.CreateOrUpdate(order));
        }

        [Fact]
        public async Task CreateOrUpdate_Success()
        {
            //arrange
            var order = new Order(
                new List<OrderItem> {
                    new OrderItem("001", "product 001", 1, 12.34m),
                    new OrderItem("002", "product 002", 2, 23.45m)
                },
                "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");

            var options = new DbContextOptionsBuilder<ApplicationContext>().UseInMemoryDatabase(databaseName: "database_name").Options;

            using (var context = new ApplicationContext(options))
            {
                var orderEntity = await context.AddAsync(order);

                var repo = new OrderRepository(context);

                //act
                Order result = await repo.CreateOrUpdate(order);

                //assert
                Assert.Equal(2, result.Items.Count);
                Assert.Collection(result.Items,
                    item => Assert.Equal("001", item.ProductCode),
                    item => Assert.Equal("002", item.ProductCode)
                );
            }

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task GetOrders_Invalid_Client(string customerId)
        {
            //arrange
            var repository = new OrderRepository(contextoMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.GetOrders(customerId));
        }
    }
}
