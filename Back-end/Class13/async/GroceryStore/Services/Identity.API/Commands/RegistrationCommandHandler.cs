using Messages.Commands;
using Messages.IntegrationEvents.Events;
using Identity.API.Managers;
using IdentityModel;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.API.Commands
{
    public class RegistrationCommandHandler
        : IRequestHandler<IdentifiedCommand<RegistrationCommand, bool>, bool>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RegistrationCommandHandler> _logger;
        private readonly IClaimsManager _claimsManager;

        public RegistrationCommandHandler(IMediator mediator, ILogger<RegistrationCommandHandler> logger, IClaimsManager claimsManager)
        {
            this._mediator = mediator;
            this._logger = logger;
            this._claimsManager = claimsManager;
        }

        public async Task<bool> Handle(IdentifiedCommand<RegistrationCommand, bool> request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException();

            var command = request.Command;
            var guid = request.Id;

            if (command == null)
                throw new ArgumentNullException();

            if (guid == Guid.Empty)
                throw new ArgumentException();

            if (string.IsNullOrWhiteSpace(command.UserId)
                 || string.IsNullOrWhiteSpace(command.Nome)
                 || string.IsNullOrWhiteSpace(command.Email)
                 || string.IsNullOrWhiteSpace(command.Phone)
                 || string.IsNullOrWhiteSpace(command.Address)
                 || string.IsNullOrWhiteSpace(command.AdditionalAddress)
                 || string.IsNullOrWhiteSpace(command.District)
                 || string.IsNullOrWhiteSpace(command.City)
                 || string.IsNullOrWhiteSpace(command.State)
                 || string.IsNullOrWhiteSpace(command.ZipCode)
                )
                throw new InvalidUserDataException();

            try
            {
                IDictionary<string, string> claims = new Dictionary<string, string>
                {
                    ["name"] = command.Nome,
                    ["email"] = command.Email,
                    ["address"] = command.Address,
                    ["address_details"] = command.AdditionalAddress,
                    ["phone"] = command.Phone,
                    ["district"] = command.District,
                    ["city"] = command.City,
                    ["state"] = command.State,
                    ["zip_code"] = command.ZipCode
                };
            
                await _claimsManager.AddUpdateClaim(command.UserId, claims);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }

    [Serializable]
    public class InvalidUserDataException : Exception
    {
        public InvalidUserDataException() { }
        public InvalidUserDataException(string message) : base(message) { }
        public InvalidUserDataException(string message, Exception inner) : base(message, inner) { }
        protected InvalidUserDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
