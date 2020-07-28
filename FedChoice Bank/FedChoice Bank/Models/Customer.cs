using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FedChoice_Bank.Models
{
    public partial class Customer
    {
        [Display(Name = "Customer SSN ID")]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Digits only")]
        [MaxLength(9, ErrorMessage ="Must be 9 digits")]
        [Required]
        public int CustomerSsn { get; set; }
        public int CustomerId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$", ErrorMessage = "Alphabets only")]
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
