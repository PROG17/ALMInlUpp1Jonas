using ALMInlUpp1Jonas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALMInlUpp1Jonas.Repositories
{
    public class BankRepository
    {
        public List<Account> Accounts { get; set; }
        public List<Customer> Customers { get; set; }

        public BankRepository()
        {
            Accounts = new List<Account>();
            Customers = new List<Customer>();

            CreateBankData();
        }

        // Den här metoden sätter in pengar på ett konto 
        public string Deposit(int accNr, decimal amount)
        {                      
            Account account = Accounts.Where(x => x.AccountNr == accNr).FirstOrDefault();

            if (account == null)
            {
                return "Kontonumret finns inte";
            }

            account.Deposit(amount);
            return "OK";
        }

        
        // Den här metoden tar ut pengar från ett konto
        public string Withdrawal(int accNr, decimal amount)
        {
            Account account = Accounts.Where(x => x.AccountNr == accNr).FirstOrDefault();

            if (account == null)
            {
                return "Kontonumret finns inte";               
            }

            // Det ska inte gå att ta ut mer pengar från kontot än vad som finns
            if (amount > account.Balance)
            {
                return "Beloppet högre än saldot på kontot";
            }

            account.Withdrawal(amount);
            return "OK";
        }

        public bool Transfer(int fromaccount, int toaccount, decimal sum)
        {
            if (fromaccount != toaccount)
            {
                Account fromAccount = Accounts.Where(a => a.AccountNr == fromaccount).FirstOrDefault();
                Account toAccount = Accounts.Where(a => a.AccountNr == toaccount).FirstOrDefault();

                if (fromAccount.Balance >= sum)
                {
                    fromAccount.Withdrawal(sum);
                    toAccount.Deposit(sum);
                }
                else
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
            
        }

        public decimal GetBalance(int accNr)
        {
            Account account = Accounts.Where(x => x.AccountNr == accNr).FirstOrDefault();
            if (account == null)
            {
                return 0;
            }
            else
            {
                return account.Balance;
            }
        }

        private void CreateBankData()
       {           

           Customer cust1 = new Customer();
           cust1.CustomerNr = 1;
           cust1.Name = "Anna";
           Customers.Add(cust1);

           Customer cust2 = new Customer();
           cust2.CustomerNr = 2;
           cust2.Name = "Britta";
           Customers.Add(cust2);    

           Customer cust3 = new Customer();
           cust3.CustomerNr = 3;
           cust3.Name = "Cecilia";
           Customers.Add(cust3);

           Account acct1 = new Account();
           acct1.AccountNr = 2001;
           acct1.Balance = 5000;
           acct1.Owner = cust2;
           Accounts.Add(acct1);

           Account acct2 = new Account();
           acct2.AccountNr = 2002;
           acct2.Balance = 8000;
           acct2.Owner = cust3;
           Accounts.Add(acct2);

           Account acct3 = new Account();
           acct3.AccountNr = 2003;
           acct3.Balance = 1;
           acct3.Owner = cust3;
           Accounts.Add(acct3);

           Account acct4 = new Account();
           acct4.AccountNr = 2004;
           acct4.Balance = 10000;
           acct4.Owner = cust1;
           Accounts.Add(acct4);
       }     
    }   
}
