using System;
using System.Collections.Generic;

namespace FedChoice_Bank.Models
{
    public partial class Customer
    {
        public int CustomerSsn { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}
