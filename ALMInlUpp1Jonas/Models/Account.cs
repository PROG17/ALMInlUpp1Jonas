using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALMInlUpp1Jonas.Models
{
    public class Account
    {
        public int AccountNr { get; set; }
        public decimal Balance { get; set; }
        public Customer Owner { get; set; }

        // Den här metoden sätter in pengar på ett konto
        internal void Deposit(decimal amount)
        {
            // Addera insatt belopp till saldot
            Balance += amount;
        }

        // Den här metoden tar ut pengar från ett konto och skapar motsvarande transaktion
        internal void Withdrawal(decimal amount)
        {
            // Subtrahera uttaget belopp från saldot
            Balance -= amount;
        }
      
    }
}
