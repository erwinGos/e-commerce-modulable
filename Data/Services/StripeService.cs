using Data.DTO.ProductDto;
using Data.Services.Contract;
using Stripe;

namespace Data.Services
{
    public class StripeService : IStripeService
    {
        private readonly Stripe.ProductService _productService;

        public StripeService(Stripe.ProductService productService)
        {
            _productService = productService;
        }

        public async Task<Stripe.Product> CreateProduct(CreateProduct product)
        {
            var productCreateOptions = new ProductCreateOptions
            {
                Name = product.ProductName,
                Description = product.Description,
                DefaultPriceData = new ProductDefaultPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "eur",
                    TaxBehavior = "inclusive"
                },
                TaxCode = "txcd_99999999"
            };
            return await _productService.CreateAsync(productCreateOptions);
        }

        public async Task<string> RemoveProduct(ProductRead product)
        {
            try
            {
                try
                {
                    await _productService.DeleteAsync(product.StripeProductId);
                    return "Produit supprimé.";
                } catch(StripeException ex)
                {
                    try
                    {
                        await _productService.UpdateAsync(product.StripeProductId, new ProductUpdateOptions { Active = false });
                        return "Produit archivé.";
                    } catch(StripeException ex2)
                    {
                        throw new Exception("Une erreur s'est produite.");
                    }
                }

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
