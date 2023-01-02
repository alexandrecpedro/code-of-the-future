using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ProductViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }
    }

    public class Item
    {
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal => Quantity * UnitPrice;
        public int Id { get; set; }
    }

    public class RegistrationViewModel : IEquatable<RegistrationViewModel>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters long")]
        [MaxLength(50, ErrorMessage = "Name must be at least 50 characters long")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = "";
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; } = "";
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = "";
        [Required(ErrorMessage = "AdditionalAddress is required")]
        public string AdditionalAddress { get; set; } = "";
        [Required(ErrorMessage = "District is required")]
        public string District { get; set; } = "";
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = "";
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; } = "";
        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; } = "";

        public RegistrationViewModel()
        {

        }

        public RegistrationViewModel(Registration registration)
        {
            this.Id = registration.Id;
            this.District = registration.District;
            this.ZipCode = registration.ZipCode;
            this.AdditionalAddress = registration.AdditionalAddress;
            this.Email = registration.Email;
            this.Address = registration.Address;
            this.City = registration.City;
            this.Name = registration.Name;
            this.Phone = registration.Phone;
            this.State = registration.State;
        }

        public bool Equals(RegistrationViewModel other)
        {
            if (other == null)
            {
                return false;
            }

            return
            this.Id == other.Id &&
            this.District == other.District &&
            this.ZipCode == other.ZipCode &&
            this.AdditionalAddress == other.AdditionalAddress &&
            this.Email == other.Email &&
            this.Address == other.Address &&
            this.City == other.City &&
            this.Name == other.Name &&
            this.Phone == other.Phone &&
            this.State == other.State;
        }
    }

    public class OrderViewModel
    {
        public List<Item> Items { get; set; }
        public int RegistrationId { get; set; }
        public RegistrationViewModel Registration { get; set; }
        public int Id { get; set; }
    }
}
