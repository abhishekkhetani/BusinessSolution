using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusinessSolution.Models
{
    public class PaymentDetails
    {
        public PaymentDetails(){}
        [Key]
        public int PaymentID { get; set; }
        
        [Required(ErrorMessage="First name is required.")]
        [DisplayName("First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Middle name is required.")]
        [DisplayName("Middle name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage="Last name is required.")]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage="Description is required.")]
        public string Description { get; set; }
        
        [Required(ErrorMessage="City is required.")]
        public string City { get; set; }
        
        [Required(ErrorMessage="Phone number is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Please enter proper phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage="Rupees is required.")]
        public string Rupees { get; set; }

        [Required(ErrorMessage="Payment Status is required.")]
        [DisplayName("Payment Status")]
        public bool PaymentStatus { get; set; }
    }
}