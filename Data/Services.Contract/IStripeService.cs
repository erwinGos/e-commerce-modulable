using Data.DTO.ProductDto;


namespace Data.Services.Contract
{
    public interface IStripeService
    {
        public Task<Stripe.Product> CreateProduct(CreateProduct product);

        public Task<string> RemoveProduct(string StripeProductId);

        public Task<Stripe.Product> UpdateProduct(ProductRead product);
    }
}
