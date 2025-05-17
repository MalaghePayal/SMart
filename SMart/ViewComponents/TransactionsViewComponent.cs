using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMart.Models;

namespace SMart.ViewComponents
{
    [ViewComponent]
    public class TransactionsViewComponent :ViewComponent
    {
        private readonly ApplicationContextDb _context;

        public TransactionsViewComponent(ApplicationContextDb context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(string cashierName, DateTime date)
        {
            var transactions = _context.Transactions
                .Where(t => t.CashierName == cashierName && t.TimeStamp.Date == date.Date)
                .OrderByDescending(t => t.TimeStamp)
                .ToList();

            return View(transactions);
        }
    }
}
