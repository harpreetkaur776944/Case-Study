using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FedChoice_Bank.Models
{
    public partial class Status
    {
      
        public int CustomerId { get; set; }

        [DisplayName("Customer SSNID")]
        public int CustomerSsnid { get; set; }
        public int? AccountId { get; set; }
        public string AccountType { get; set; }

        [DisplayName("Status")]
        public string Status1 { get; set; }
        public string Message { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
