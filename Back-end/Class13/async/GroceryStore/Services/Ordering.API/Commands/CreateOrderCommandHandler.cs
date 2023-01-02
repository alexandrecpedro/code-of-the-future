using Messages.Commands;
using Ordering.Models;
using Ordering.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;

namespace Ordering.Commands
{
    public class CreateOrderCommandHandler
        : IRequestHandler<IdentifiedCommand<CreateOrderCommand, bool>, bool>
    {
        private EventId EventId_CreateOrder = new EventId(1003, "Checkout");
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;
        private readonly HubConnection _connection;

        public CreateOrderCommandHandler(
            ILogger<CreateOrderCommandHandler> logger
            , IOrderRepository orderRepository
            , IBus bus
            , IConfiguration configuration
            , IDistributedCache cache
            )
        {
            this._orderRepository = orderRepository;
            this._logger = logger;
            this._bus = bus;
            this._configuration = configuration;
            this._cache = cache;
            string userCounterDataHubUrl = $"{_configuration["SignalRServerUrl"]}usercounterdatahub";

            this._connection = new HubConnectionBuilder()
                .WithUrl(userCounterDataHubUrl, HttpTransportType.WebSockets)
                .Build();
            this._connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await this._connection.StartAsync();
            };

            this._connection.StartAsync().GetAwaiter();
        }

        public async Task<bool> Handle(IdentifiedCommand<CreateOrderCommand, bool> request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException("Request cannot be empty");

            var cmd = request.Command;
            var guid = request.Id;

            if (!string.IsNullOrWhiteSpace
                (await this._cache.GetStringAsync(
                    cmd.IdempotencyId.ToString())))
            {
                return true;
            }

            await this._cache.SetStringAsync(
                    cmd.IdempotencyId.ToString(), 
                    DateTime.Now.ToString());

            if (cmd == null)
                throw new ArgumentNullException("Command cannot be empty");

            if (guid == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty");

            var items = cmd.Items.Select(
                    i => new OrderItem(i.ProductCode, i.ProductName, i.ProductQuantity, i.ProductUnitPrice)
                ).ToList();

            if (items.Count == 0)
            {
                throw new NoItemsException();
            }


            foreach (var item in items)
            {
                if (
                    string.IsNullOrWhiteSpace(item.ProductCode)
                    || string.IsNullOrWhiteSpace(item.ProductName)
                    || item.ProductQuantity <= 0
                    || item.ProductUnitPrice <= 0
                    )
                {
                    throw new InvalidItemException();
                }
            }


            if (string.IsNullOrWhiteSpace(cmd.CustomerId)
                 || string.IsNullOrWhiteSpace(cmd.CustomerName)
                 || string.IsNullOrWhiteSpace(cmd.CustomerEmail)
                 || string.IsNullOrWhiteSpace(cmd.CustomerPhone)
                 || string.IsNullOrWhiteSpace(cmd.CustomerAddress)
                 || string.IsNullOrWhiteSpace(cmd.CustomerAdditionalCustomer)
                 || string.IsNullOrWhiteSpace(cmd.CustomerDistrict)
                 || string.IsNullOrWhiteSpace(cmd.CustomerCity)
                 || string.IsNullOrWhiteSpace(cmd.CustomerState)
                 || string.IsNullOrWhiteSpace(cmd.CustomerZipCode)
                )
                throw new InvalidUserDataException();

            var order = new Order(items, cmd.CustomerId,
                cmd.CustomerName, cmd.CustomerEmail, cmd.CustomerPhone, 
                cmd.CustomerAddress, cmd.CustomerAdditionalCustomer, cmd.CustomerDistrict, 
                cmd.CustomerCity, cmd.CustomerState, cmd.CustomerZipCode);
            order.DateCreated = DateTime.Now;

            try
            {
                Order newOrder = null;
                using (var activitySource = new ActivitySource("Ordering.API"))
                {
                    using(var activity = activitySource.StartActivity("NewOrder"))
                    {
                        newOrder = await this._orderRepository.CreateOrUpdate(order);
                        activity?.SetTag("Customer.Id", order.CustomerId);
                        activity?.SetTag("Order.Id", order.Id);
                    }
                }

                string notificationText = $"New order placed successfully: {newOrder.Id}";

                _logger.LogInformation(eventId: EventId_CreateOrder, message: "New order has been placed successfully: {Order}", newOrder);

                HttpClient httpClient = new HttpClient();

                await this._connection.InvokeAsync("SendUserNotification",
                    $"{newOrder.CustomerId}", notificationText);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
