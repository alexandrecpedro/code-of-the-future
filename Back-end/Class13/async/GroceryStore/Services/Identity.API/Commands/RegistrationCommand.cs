using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Commands
{
    public class RegistrationCommand : IRequest<bool>
    {
        public RegistrationCommand()
        {

        }

        public RegistrationCommand(string userId, string nome, string email, string phone, string address, string additionalAddress, string district, string city, string state, string zipCode)
        {
            UserId = userId;
            Nome = nome;
            Email = email;
            Phone = phone;
            Address = address;
            AdditionalAddress = additionalAddress;
            District = district;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string UserId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AdditionalAddress { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
