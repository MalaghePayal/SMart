using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMart.Models;
using SMart.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SMart.Controllers
{
    //[Authorize]
    public class TransactionsController : Controller
    {
        private readonly ApplicationContextDb _context;

        public TransactionsController(ApplicationContextDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TransactionViewModel transactionViewModel = new TransactionViewModel();
           
            
            return View(transactionViewModel);
        }

        
        public IActionResult Search(TransactionViewModel transactionViewModel)
        {
            var transactions = _context.Transactions.AsQueryable();

            // Filter by cashier name if it's provided
            if (!string.IsNullOrWhiteSpace(transactionViewModel.CashierName))
            {
                transactions = transactions.Where(t =>
                    t.CashierName.ToLower().Contains(transactionViewModel.CashierName.ToLower()));
            }

            // Filter by start and end date if they are provided
            if (transactionViewModel.StartDate != DateTime.MinValue && transactionViewModel.EndDate != DateTime.MinValue)
            {
                transactions = transactions.Where(t =>
                    t.TimeStamp >= transactionViewModel.StartDate.Date &&
                    t.TimeStamp <= transactionViewModel.EndDate.Date.AddDays(1));
            }

            // Assign the result to the view model
            transactionViewModel.Transactions = transactions
                .OrderByDescending(t => t.TimeStamp)
                .ToList();

            return View("Index", transactionViewModel);
        }
    }
}
