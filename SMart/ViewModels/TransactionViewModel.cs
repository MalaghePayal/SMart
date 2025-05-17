using System.ComponentModel;
using SMart.Models;

namespace SMart.ViewModels
{
    public class TransactionViewModel
    {
        [DisplayName("Cashier Name")]
        public string? CashierName { get; set; }
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; } = DateTime.Today;

       
        public IEnumerable<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
//Add-Migration InitIdentity -context AccountContext
//Update-Database -Context AccountContext