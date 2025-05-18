using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMart.Models;
using SMart.ViewModels;

namespace SMart.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly ApplicationContextDb _context;

        public SalesController(ApplicationContextDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            SalesViewModel salesViewModel = new SalesViewModel()
            {
                CategoriesList = _context.Categories.ToList()
            };
            return View(salesViewModel);
        }

        public IActionResult GetProductDetailBySelectedProdIdPartial(int productId)
        {
            var Product = _context.Products.Find(productId);
            if (Product != null)
            {
                return PartialView("_GetProductDetailBySelectedProdIdPartial", Product);
            }
            return NotFound();
        }

        public IActionResult sell(SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid)
            {
                // sell the product
                var prod = _context.Products.Find(salesViewModel.SelectedProductId);
                if (prod != null)
                {
                    var transaction = new Transaction
                    {
                        CashierName = "Cashier1",
                        ProductId = salesViewModel.SelectedProductId,
                        ProductName = prod.Name,
                        Price = prod.Price,
                        BeforeQty = prod.Quantity,
                        SoldQty = salesViewModel.QuantityToSell,
                        TimeStamp = DateTime.Now
                    };
                    //if (_context.Transactions !=null && _context.Transactions.Count() > 0)
                    //{
                    //    var maxId = _context.Transactions.Max(x => x.TransactionId);
                    //    transaction.TransactionId = maxId + 1;

                    //}
                    //else
                    //{
                    //    transaction.TransactionId =  1;
                    //}
                    

                    
                        prod.Quantity -= salesViewModel.QuantityToSell;
                    

                    // Save to database
                    _context.Transactions?.Add(transaction);
                    _context.Products.Update(prod);
                    _context.SaveChanges();
                }
            }

            var product = _context.Products.Find(salesViewModel.SelectedProductId);
            salesViewModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId.Value;
            
            salesViewModel.CategoriesList = _context.Categories.ToList();
            return View("Index", salesViewModel);
        }
    }
}
