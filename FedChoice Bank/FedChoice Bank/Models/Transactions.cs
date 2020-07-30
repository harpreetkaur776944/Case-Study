using System;
using System.Collections.Generic;

namespace FedChoice_Bank.Models
{
    public partial class Transactions
    {
        public int AccountId { get; set; }
        public string AccountType { get; set; }
        public int? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int? TargetAccountId { get; set; }
        public string TargetAccountType { get; set; }
        public int TransactionId { get; set; }

        public string Description { get; set; }

    }
}
