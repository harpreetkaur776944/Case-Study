using System;
using System.Collections.Generic;

namespace FedChoice_Bank.Models
{
    public partial class Status
    {
        public int CustomerId { get; set; }
        public int CustomerSsnid { get; set; }
        public int? AccountId { get; set; }
        public string AccountType { get; set; }
        public string Status1 { get; set; }
        public string Message { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
