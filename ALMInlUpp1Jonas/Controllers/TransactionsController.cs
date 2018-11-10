using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALMInlUpp1Jonas.Repositories;
using ALMInlUpp1Jonas.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ALMInlUpp1Jonas.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly BankRepository _bank;

        public TransactionsController(BankRepository bank)
        {
            _bank = bank;
        }

        [HttpGet]
        public IActionResult DepositOrWithdrawal()
        {
            CreateTransactionViewModel viewModel = new CreateTransactionViewModel();            
            return View(viewModel);
        }

        // Gör en Insättning
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit([Bind("AccountNr,Amount")] CreateTransactionViewModel viewModel)
        {          
            if (ModelState.IsValid)
            {
                if (decimal.TryParse(viewModel.Amount, out decimal amount) == true)
                {
                    // Gör en insättning i Banken
                    string result = _bank.Deposit(viewModel.AccountNr, amount);

                    if (result == "OK")
                    {
                        viewModel.TransactionResult = "Efter insättning har konto: " + viewModel.AccountNr + " det nya saldot " + _bank.GetBalance(viewModel.AccountNr) + " Kr.";                      
                    }
                    else
                    {
                        AddError(result);                        
                    }
                    return View("DepositOrWithdrawal", viewModel);
                }


                AddError("Fel format på Belopp");
                return View("DepositOrWithdrawal", viewModel);
            }
                
            return View("DepositOrWithdrawal", viewModel);
        }

        // Gör ett Uttag
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdrawal([Bind("AccountNr,Amount")] CreateTransactionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (decimal.TryParse(viewModel.Amount, out decimal amount) == true)
                {
                    // Gör en insättning i Banken
                    string result = _bank.Withdrawal(viewModel.AccountNr, amount);

                    if (result == "OK")
                    {
                        viewModel.TransactionResult = "Efter uttag har konto: " + viewModel.AccountNr + " det nya saldot " + _bank.GetBalance(viewModel.AccountNr) + " Kr.";
                    }
                    else
                    {
                        AddError(result);
                    }
                    return View("DepositOrWithdrawal", viewModel);
                }


                AddError("Fel format på Belopp");
                return View("DepositOrWithdrawal", viewModel);
            }

            return View("DepositOrWithdrawal", viewModel);
        }
        // Visa felmeddelande
        private void AddError(string errorDescription)
        {
            ModelState.AddModelError(string.Empty, errorDescription);
        }

    }
}