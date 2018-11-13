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

        [Theory]
        [InlineData(2004, 2004, 1, false)] //same account == false
        [InlineData(2004, 2001, 1, true)] //other account == true
        public void Transfer(int fromaccount, int toaccount, int sum, bool expectedResult)
        {

            // act
            var result = _bank.Transfer(fromaccount, toaccount, sum);

            // assert            
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(100, 50, true)] // balance higher than sum == true
        [InlineData(50, 100, false)] // balance lower than sum == false
        public void Transfer_Not_too_much(int fromBalance, decimal sum, bool expected)
        {
            //arrange
            Customer c1 = new Customer() {
                Name = "Person",
                CustomerNr = 1
            };           
            Customer c2 = new Customer()
            {
                Name = "Person2",
                CustomerNr = 2
            };

            _bank.Customers.Add(c1);
            _bank.Customers.Add(c2);

            Account fromAccount = new Account() {
                AccountNr = 1,
                Owner = c1,
                Balance = fromBalance
            };

            Account toAccount = new Account()
            {
                AccountNr = 2,
                Owner = c2,
                Balance = 0
            };
            _bank.Accounts.Add(fromAccount);
            _bank.Accounts.Add(toAccount);

            // act
            var result = _bank.Transfer(1, 2, sum);

            // assert            
            Assert.Equal(expected, result);
        }
    }
}
