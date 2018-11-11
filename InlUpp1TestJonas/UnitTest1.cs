using ALMInlUpp1Jonas.Models;
using ALMInlUpp1Jonas.Repositories;
using Xunit;

namespace InlUpp1UnitTestJonas
{
    public class UnitTest1
    {
        private readonly BankRepository _bank;      

        public UnitTest1()
        {
            _bank = new BankRepository();           
        }

        [Theory]
        [InlineData(100, 99, 199)]
        [InlineData(1, 2000, 2001)]
        [InlineData(10000, 10001, 20001)]
        public void DepositShouldSucceed(decimal balance, decimal amount, decimal expectedValue) 
        {
            // arrange
            Customer cust = new Customer();
            cust.CustomerNr = 1;
            cust.Name = "A";
            _bank.Customers.Add(cust);

            int accountNr = 1;
            Account acct = new Account();
            acct.AccountNr = accountNr;
            acct.Balance = balance;
            acct.Owner = cust;
            _bank.Accounts.Add(acct);

            // act
            string actualResult = _bank.Deposit(accountNr, amount);

            // assert
            string expectedResult = "OK";
            Assert.Equal(expectedResult, actualResult);
            decimal actualValue = acct.Balance;
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(100, 99, 1)]
        [InlineData(2000, 1, 1999)]
        [InlineData(100001, 100001, 0)]
        public void WithdrawalShouldSucceed(decimal balance, decimal amount, decimal expectedValue)
        {
            // arrange
            Customer cust = new Customer();
            cust.CustomerNr = 1;
            cust.Name = "A";
            _bank.Customers.Add(cust);

            int accountNr = 1;
            Account acct = new Account();
            acct.AccountNr = accountNr;
            acct.Balance = balance;
            acct.Owner = cust;
            _bank.Accounts.Add(acct);

            // act
            string actualResult = _bank.Withdrawal(accountNr, amount);

            // assert
            string expectedResult = "OK";
            Assert.Equal(expectedResult, actualResult);
            decimal actualValue = acct.Balance;
            Assert.Equal(expectedValue, actualValue);
        }

        [Theory]
        [InlineData(99, 100, "Beloppet högre än saldot på kontot")]
        [InlineData(1, 2000, "Beloppet högre än saldot på kontot")]
        [InlineData(10000, 100001, "Beloppet högre än saldot på kontot")]
        public void WithdrawalShouldFail(decimal balance, decimal amount, string expectedResult)
        {
            // arrange
            Customer cust = new Customer();
            cust.CustomerNr = 1;
            cust.Name = "A";
            _bank.Customers.Add(cust);

            int accountNr = 1;
            Account acct = new Account();
            acct.AccountNr = accountNr;
            acct.Balance = balance;
            acct.Owner = cust;
            _bank.Accounts.Add(acct);

            // act
            string actualResult = _bank.Withdrawal(accountNr, amount);

            // assert            
            Assert.Equal(expectedResult, actualResult);           
        }
    }
}
