using Messages.Commands;
using Identity.API.Commands;
using Identity.API.Managers;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Identity.UnitTests
{
    public class RegistrationCommandHandlerTest
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly Mock<ILogger<RegistrationCommandHandler>> loggerMock;
        private readonly Mock<IClaimsManager> claimsManagerMock;

        public RegistrationCommandHandlerTest()
        {
            this.mediatorMock = new Mock<IMediator>();
            this.loggerMock = new Mock<ILogger<RegistrationCommandHandler>>();
            this.claimsManagerMock = new Mock<IClaimsManager>();
        }

        [Fact]
        public async Task Handle_request_is_null()
        {
            //arrange
            var handler = new RegistrationCommandHandler(mediatorMock.Object, loggerMock.Object, claimsManagerMock.Object);

            IdentifiedCommand<RegistrationCommand, bool> request = null;
            CancellationToken token = default(System.Threading.CancellationToken);
            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(request, token));
        }

        [Fact]
        public async Task Handle_command_is_null()
        {
            //arrange
            var handler = new RegistrationCommandHandler(mediatorMock.Object, loggerMock.Object, claimsManagerMock.Object);

            IdentifiedCommand<RegistrationCommand, bool> request = new IdentifiedCommand<RegistrationCommand, bool>(null, Guid.NewGuid());
            CancellationToken token = default(System.Threading.CancellationToken);
            //act
            //assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(request, token));
        }

        [Fact]
        public async Task Handle_guid_is_empty()
        {
            //arrange
            var handler = new RegistrationCommandHandler(mediatorMock.Object, loggerMock.Object, claimsManagerMock.Object);

            IdentifiedCommand<RegistrationCommand, bool> request = new IdentifiedCommand<RegistrationCommand, bool>(new RegistrationCommand(), Guid.Empty);
            CancellationToken token = default(System.Threading.CancellationToken);
            //act
            //assert
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(request, token));
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
            var handler = new RegistrationCommandHandler(mediatorMock.Object, loggerMock.Object, claimsManagerMock.Object);

            RegistrationCommand command = new RegistrationCommand(customerId, customerName, customerEmail, customerPhone, customerAddress, customerAdditionalAddress, customerDistrict, customerCity, customerState, customerZipCode);
            IdentifiedCommand<RegistrationCommand, bool> request = new IdentifiedCommand<RegistrationCommand, bool>(command, Guid.NewGuid());
            CancellationToken token = default(System.Threading.CancellationToken);
            //act
            //assert
            await Assert.ThrowsAsync<InvalidUserDataException>(() => handler.Handle(request, token));
        }

        [Fact]
        public async Task Handle_success()
        {
            //arrange
            claimsManagerMock
                .Setup(c => c.AddUpdateClaim(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
                .Returns(Task.CompletedTask)
               .Verifiable();

            var handler = new RegistrationCommandHandler(mediatorMock.Object, loggerMock.Object, claimsManagerMock.Object);
            RegistrationCommand command = new RegistrationCommand("customerId", "customerName", "customer@email.com", "phone", "address", "additionalAddress", "district", "city", "state", "12345-678");

            IdentifiedCommand<RegistrationCommand, bool> request = new IdentifiedCommand<RegistrationCommand, bool>(command, Guid.NewGuid());
            CancellationToken token = default(CancellationToken);
            
            //act
            var result = await handler.Handle(request, token);

            //assert
            Assert.True(result);
            claimsManagerMock.Verify();
        }
    }
}
