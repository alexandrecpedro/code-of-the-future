using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Model
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string AdditionalAddress { get; set; } = "";
        public string District { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";

        public RegistrationViewModel()
        {

        }
    }
}
