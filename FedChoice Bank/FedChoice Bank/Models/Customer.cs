using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FedChoice_Bank.Models
{
    public class Customer
    {
        public int SSMid { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public string address { get; set; }
        public string state { get; set; }

        public string city { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
