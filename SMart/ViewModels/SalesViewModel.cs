using SMart.Models;
using SMart.ViewModels.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SMart.ViewModels
{
    public class SalesViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<Category> CategoriesList { get; set; } = new List<Category>();

        public int SelectedProductId { get; set; }
        [Range(1,int.MaxValue)]
        [DisplayName("Quantity")]
        [SalesViewModel_EnsureProperQuantity]
        public int QuantityToSell { get; set; }
    }
}
