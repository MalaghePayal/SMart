using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMart.Models;
using SMart.ViewModels;

namespace SMart.Controllers
{
    [Authorize(Policy= "Inventory")]
    public class ProductsController : Controller
    {
        private readonly ApplicationContextDb _context;

        public ProductsController(ApplicationContextDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var products = _context.Products
                           .Include(p => p.Category) // Important!
                           .ToList();

            return View(products);
            
        }
        [HttpGet]
        public IActionResult Add()
        {
            var ProductViewModel = new ProductViewModel
            {
                CategoriesList = _context.Categories.ToList()
            };
            
            return View(ProductViewModel);
        }
        [HttpPost]
        public IActionResult Add( ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(model.Product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            model.CategoriesList = _context.Categories.ToList();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var productByID = _context.Products.Find(id);
            if (productByID != null)
            {
                var viewModel = new ProductViewModel
                {
                    Product = productByID,
                    CategoriesList = _context.Categories.ToList() // or whatever logic you use to populate it
                };

                return View(viewModel);
            }
            else
            {
                return NotFound();

            }

        }
        [HttpPost]
      
        public IActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _context.Products.Find(model.Product.ProductId); // Ensure this is your PK

                if (existingProduct != null)
                {
                    // Manually update only the fields you want to allow changes for
                    existingProduct.Name = model.Product.Name;
                    existingProduct.Price = model.Product.Price;
                    existingProduct.Quantity = model.Product.Quantity;
                    existingProduct.CategoryId = model.Product.CategoryId;

                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }

            // Repopulate CategoriesList in case of validation error
            model.CategoriesList = _context.Categories.ToList();
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var productByID = _context.Products.Find(id);
            if (productByID != null)
            {
                _context.Products.Remove(productByID);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();

            }
        }

        
        public IActionResult GetProductsByCategoryIdPartial(int categoryId)
        {
            var products = _context.Products
                                   .Include(p => p.Category)
                                   .Where(p => p.CategoryId == categoryId)
                                   .ToList();

            if (products == null || !products.Any())
            {
                return NotFound("No products found for this category.");
            }

              return PartialView("_GetProductsByCategoryId", products);
        }


    }
}
