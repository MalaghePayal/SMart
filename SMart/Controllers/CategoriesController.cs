using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMart.Models;

namespace SMart.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ApplicationContextDb _context;

        public CategoriesController(ApplicationContextDb context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> Categories = _context.Categories.ToList();

            return View(Categories);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
     [HttpPost]
        public IActionResult Add([Bind("Name,Description")]Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var categoryByID = _context.Categories.Find(id);
            if (categoryByID != null)
            {
                return View(categoryByID);
            }
            else
            {
                return NotFound();

            }
            
        }
        [HttpPost]
        public IActionResult Edit([Bind("CategoryId,Name,Description")] Category model)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(model);
            }
        
        }

        public IActionResult Delete(int id) 
        {
            var categoryByID = _context.Categories.Find(id);
            if (categoryByID != null)
            {
                _context.Categories.Remove(categoryByID);
                
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();

            }
        }
       
    }
}
