using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ALMInlUpp1Jonas.Models;
using ALMInlUpp1Jonas.Repositories;
using ALMInlUpp1Jonas.ViewModels;

namespace ALMInlUpp1Jonas.Controllers
{
    public class HomeController : Controller
    {
        private readonly BankRepository _bank;

        public HomeController(BankRepository bank)
        {
            _bank = bank;
        }

        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.CurrentAccounts = _bank.Accounts.OrderBy(a => a.Owner.Name).ToList();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }   
}
