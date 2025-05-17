using System.ComponentModel.DataAnnotations;
using  SMart.Models;

namespace SMart.ViewModels.Validations
{
    public class SalesViewModel_EnsureProperQuantity :ValidationAttribute
    {
     

        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var salesViewModel = validationContext.ObjectInstance as SalesViewModel;
            if (salesViewModel != null) 
            {
                if (salesViewModel.QuantityToSell < 0 )
                {
                    return new ValidationResult("Quantity to sell must be greater than zero");
                }
                else
                {
                    // Get the DbContext using service provider
                    var _context = validationContext.GetService(typeof(ApplicationContextDb)) as ApplicationContextDb;

                    if (_context == null)
                    {
                        return new ValidationResult("Database context is not available");
                    }

                    var product = _context.Products.Find(salesViewModel.SelectedProductId);
                    
                    if (product != null) 
                    {
                        if (product.Quantity < salesViewModel.QuantityToSell)
                        {
                            return new ValidationResult($"{product.Name} only has {product.Quantity} quantity left in stock");
                        }
                    }
                    else
                    {
                        return new ValidationResult("Selected product doesn't exist");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
