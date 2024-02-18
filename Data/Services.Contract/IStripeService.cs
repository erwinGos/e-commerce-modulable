using Data.DTO.ProductDto;
using Database.Entities;


namespace Data.Services.Contract
{
    public interface IStripeService
    {
        public Task<string> CreatePaymentLink(Order order, User user);
    }
}
