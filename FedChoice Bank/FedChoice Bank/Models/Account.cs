using System;
using System.Collections.Generic;

namespace FedChoice_Bank.Models
{
    public partial class Account
    {
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public int? Balance { get; set; }
        public DateTime? Crdate { get; set; }
        public DateTime? CrlastDate { get; set; }
        public int? Duration { get; set; }
    }

}
