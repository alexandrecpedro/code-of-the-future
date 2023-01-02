using Messages.Events;
using Newtonsoft.Json;

namespace Messages.IntegrationEvents.Events
{
    public class RegistryEvent : IntegrationEvent
    {
        public RegistryEvent()
        {

        }

        public RegistryEvent(string usuarioId, string name, string email, string phone, string address, string additionalAddress, string district, string city, string state, string zipCode)
        {
            UserId = usuarioId;
            Name = name;
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
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AdditionalAddress { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
