using Messages.Events;
using Messages.IntegrationEvents;
using Ordering.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using System.Linq;

namespace Messages.EventHandling
{
    /// <image src="$(ProjectDir)\eb.png"/>
    public class CheckoutEventHandler : BaseEventHandler<CheckoutEvent, CreateOrderCommand>, IHandleMessages<CheckoutEvent>
    {
        public CheckoutEventHandler(IMediator mediator, ILogger<CheckoutEventHandler> logger)
            : base(mediator, logger)
        {
        }

        protected override CreateOrderCommand GetCommand(CheckoutEvent message)
        {
            var items = message.Items.Select(
                    i => new CreateOrderCommandItem(i.ProductId, i.ProductName, i.Quantity, i.UnitPrice)
                ).ToList();

            var command = new CreateOrderCommand(message.RequestId, items, message.UserId, message.UserName, message.Email, message.Phone, message.Address, message.AdditionalAddress, message.District, message.City, message.State, message.ZipCode);
            return command;
        }
    }
}
