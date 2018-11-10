using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALMInlUpp1Jonas.ViewModels
{
    public class CreateTransactionViewModel
    {
        [DisplayName("Kontonummer:")]
        [Required(ErrorMessage = "Kontonummer är obligatoriskt")]
        public int AccountNr { get; set; }

        [DisplayName("Belopp")]
        [Required(ErrorMessage = "Belopp är obligatoriskt")]     
        public string Amount { get; set; }

        public string TransactionResult { get; set; }
    }   
}
