using System;
using System.Collections.Generic;

namespace FedChoice_Bank.Models
{
    public partial class Userstore
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
