using SMart.Models;

namespace SMart.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Category> CategoriesList { get; set; } = new List<Category>();
        public Product Product { get; set; } = new Product();
    }
}
