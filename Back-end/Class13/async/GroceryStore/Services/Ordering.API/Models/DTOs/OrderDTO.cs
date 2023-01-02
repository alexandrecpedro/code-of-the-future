using System;
using System.Collections.Generic;

namespace Ordering.Models.DTOs
{
    public class OrderDTO
    {
        public OrderDTO()
        {

        }

        public OrderDTO(List<OrderItemDTO> items, string id, string name, string email, string phone, string address, string additionalAddress, string district, string city, string state, string zipCode)
        {
            Items = items;
            Id = id;
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

        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string AdditionalAddress { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
