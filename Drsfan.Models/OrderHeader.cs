using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }

        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }

        public string? TrackingNumber { get; set; } 
        public string? Carrier { get; set; }

        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }

        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Street address is required.")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Postal code is required.")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
