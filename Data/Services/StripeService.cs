using Data.DTO.ProductDto;
using Data.Services.Contract;
using Stripe;

namespace Data.Services
{
    public class StripeService : IStripeService
    {
        private readonly Stripe.ProductService _productService;
        private readonly Stripe.PriceService _priceService;

        public StripeService(Stripe.ProductService productService, Stripe.PriceService priceService)
        {
            _productService = productService;
            _priceService = priceService;
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

        public async Task<string> RemoveProduct(string StripeProductId)
        {
            try
            {
                try
                {
                    StripeList<Price> prices = _priceService.List(new PriceListOptions { Product = StripeProductId });


                    await _productService.DeleteAsync(StripeProductId);
                    return "Produit supprimé.";
                } catch(StripeException ex)
                {
                    try
                    {
                        await _productService.UpdateAsync(StripeProductId, new ProductUpdateOptions { Active = false });
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
