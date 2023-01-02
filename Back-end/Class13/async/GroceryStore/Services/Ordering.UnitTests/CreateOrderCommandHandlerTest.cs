using Messages.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Ordering.Commands;
using Ordering.Models;
using Ordering.Repositories;
using Rebus.Bus;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ordering.UnitTests
{
    public class CreateOrderCommandHandlerTest
    {
        private readonly Mock<ILogger<CreateOrderCommandHandler>> loggerMock;
        private readonly Mock<IOrderRepository> orderRepositoryMock;
        private readonly Mock<IBus> busMock;
        private readonly Mock<IConfiguration> configurationMock;

        public CreateOrderCommandHandlerTest()
        {
            this.loggerMock = new Mock<ILogger<CreateOrderCommandHandler>>();
            this.orderRepositoryMock = new Mock<IOrderRepository>();
            this.busMock = new Mock<IBus>();
            this.configurationMock = new Mock<IConfiguration>();
        }

        [Fact]
        public async Task Handle_request_is_null()
        {
            //arrange
            CancellationToken token = default(System.Threading.CancellationToken);
            IdentifiedCommand<CreateOrderCommand, bool> request = null;
            var handler = new CreateOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, busMock.Object, configurationMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(request, token));
        }

        [Fact]
        public async Task Handle_command_is_null()
        {
            //arrange
            CancellationToken token = default(System.Threading.CancellationToken);
            IdentifiedCommand<CreateOrderCommand, bool> request = new IdentifiedCommand<CreateOrderCommand, bool>(null, new Guid());
            var handler = new CreateOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, busMock.Object, configurationMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await handler.Handle(request, token));
        }

        [Fact]
        public async Task Handle_guid_is_empty()
        {
            //arrange
            CancellationToken token = default(System.Threading.CancellationToken);
            IdentifiedCommand<CreateOrderCommand, bool> request = new IdentifiedCommand<CreateOrderCommand, bool>(new CreateOrderCommand(), Guid.Empty);
            var handler = new CreateOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, busMock.Object, configurationMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(request, token));
        }

        [Fact]
        public async Task Handle_items_is_empty()
        {
            //arrange
            CancellationToken token = default(CancellationToken);
            CreateOrderCommand command = new CreateOrderCommand();
            IdentifiedCommand<CreateOrderCommand, bool> request = new IdentifiedCommand<CreateOrderCommand, bool>(command, Guid.NewGuid());
            var handler = new CreateOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, busMock.Object, configurationMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<NoItemsException>(async () => await handler.Handle(request, token));
        }

        [Theory]
        [InlineData("", "product 001", 1, 12.34)]
        [InlineData("001", "", 1, 12.34)]
        [InlineData("001", "product 001", 0, 12.34)]
        [InlineData("001", "product 001", -1, 12.34)]
        [InlineData("001", "product 001", 1, -10)]
        public async Task Handle_invalid_item(string productCodigo, string productNome, int productQuantity, decimal productUnitPrice)
        {
            //arrange
            CancellationToken token = default(CancellationToken);
            CreateOrderCommand command = new CreateOrderCommand(new List<CreateOrderCommandItem>
            {
                new CreateOrderCommandItem(productCodigo, productNome, productQuantity, productUnitPrice)
            }
            , "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");
            IdentifiedCommand<CreateOrderCommand, bool> request = new IdentifiedCommand<CreateOrderCommand, bool>(command, Guid.NewGuid());
            var handler = new CreateOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, busMock.Object, configurationMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<InvalidItemException>(async () => await handler.Handle(request, token));
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
        public async Task Handle_invalid_user_data(string customerId, string customerName, string customerEmail, string customerPhone, string customerAddress, string customerAdditionalAddress, string customerDistrict, string customerCity, string customerState, string customerZipCode)
        {
            //arrange
            CancellationToken token = default(CancellationToken);
            CreateOrderCommand command = new CreateOrderCommand(new List<CreateOrderCommandItem>
            {
                new CreateOrderCommandItem("001", "product 001", 1, 12.34m)
            }
            , customerId, customerName, customerEmail, customerPhone, customerAddress, customerAdditionalAddress, customerDistrict, customerCity, customerState, customerZipCode);
            IdentifiedCommand<CreateOrderCommand, bool> request = new IdentifiedCommand<CreateOrderCommand, bool>(command, Guid.NewGuid());
            var handler = new CreateOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, busMock.Object, configurationMock.Object);

            //act
            //assert
            await Assert.ThrowsAsync<InvalidUserDataException>(async () => await handler.Handle(request, token));
        }

        [Fact]
        public async Task Handle_success()
        {
            //arrange
            var order = new Order(
                new List<OrderItem> {
                    new OrderItem("001", "product 001", 1, 12.34m),
                    new OrderItem("002", "product 002", 2, 23.45m)
                },
                "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");

            CancellationToken token = default(CancellationToken);
            CreateOrderCommand command = new CreateOrderCommand(new List<CreateOrderCommandItem>
            {
                new CreateOrderCommandItem("001", "product 001", 1, 12.34m),
                new CreateOrderCommandItem("002", "product 002", 2, 23.45m)
            }
            , "customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");
            IdentifiedCommand<CreateOrderCommand, bool> request = new IdentifiedCommand<CreateOrderCommand, bool>(command, Guid.NewGuid());
            orderRepositoryMock
                .Setup(r => r.CreateOrUpdate(It.IsAny<Order>()))
                .ReturnsAsync(order)
                .Verifiable();

            var handler = new CreateOrderCommandHandler(loggerMock.Object, orderRepositoryMock.Object, busMock.Object, configurationMock.Object);

            //act
            bool result = await handler.Handle(request, token);

            //assert
            Assert.True(result);

            orderRepositoryMock.Verify();
        }
    }
}
