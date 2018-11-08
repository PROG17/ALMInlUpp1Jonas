using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALMInlUpp1Jonas.Models
{
    public class Account
    {
        public int AccountNr { get; internal set; }
        public decimal Balance { get; internal set; }
        public Customer Owner { get; internal set; }
    }
}
