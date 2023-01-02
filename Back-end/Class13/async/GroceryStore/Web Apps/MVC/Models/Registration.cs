using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Registration : BaseModel
    {
        public Registration()
        {
        }

        public virtual Order Order { get; set; }
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

        internal void Update(Registration newRegistration)
        {
            this.District = newRegistration.District;
            this.ZipCode = newRegistration.ZipCode;
            this.AdditionalAddress = newRegistration.AdditionalAddress;
            this.Email = newRegistration.Email;
            this.Address = newRegistration.Address;
            this.City = newRegistration.City;
            this.Name = newRegistration.Name;
            this.Phone = newRegistration.Phone;
            this.State = newRegistration.State;
        }
    }
}
