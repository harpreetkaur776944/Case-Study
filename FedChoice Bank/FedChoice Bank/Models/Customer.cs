using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FedChoice_Bank.Models
{
    public partial class Customer
    {
        [Display(Name = "Customer SSN ID")]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Digits only")]
        [Range(100000000, 999999999, ErrorMessage = "Invalid SSN Number")]
        [Required]
        public int CustomerSsn { get; set; }
        public int CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        [Range(1, 100 , ErrorMessage = "Invalid Age")]
        public int Age { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
    }

}
