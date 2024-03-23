using Data.DTO.ProductDto;
using Data.Services.Contract;
using Database.Entities;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Climate;
using Stripe.TestHelpers;

namespace Data.Services
{
    public class StripeService : IStripeService
    {
        private readonly Stripe.ProductService _productService;
        private readonly Stripe.PriceService _priceService;
        private readonly Stripe.Checkout.SessionService _sessionService;
        private readonly Stripe.CustomerService _customerService;
        private readonly IConfiguration _configuration;

        public StripeService(IConfiguration configuration, Stripe.Checkout.SessionService sessionService, Stripe.ProductService productService, Stripe.PriceService priceService, Stripe.CustomerService customerService)
        {
            _productService = productService;
            _priceService = priceService;
            _sessionService = sessionService;
            _customerService = customerService;
            _configuration = configuration;
        }

        public async Task<string> CreatePaymentLink(Database.Entities.Order order, User user)
        {
            try
            {
                Price freshlyCreatedPrice = _priceService.Create(new PriceCreateOptions()
                {
                    ProductData = new PriceProductDataOptions
                    {
                        Name = "order_" + order.OrderNumber + "_" + order.UserId,
                    },
                    Currency = "eur",
                    UnitAmountDecimal = order.Total * 100,
                    TaxBehavior = "inclusive"
                });

                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    SuccessUrl = _configuration["Urls:frontEndUrl"] + "/ordersuccesspayment/" + order.OrderNumber,
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>
                {
                    new Stripe.Checkout.SessionLineItemOptions
                    {
                        Price = freshlyCreatedPrice.Id,
                        Quantity = 1,
                    },
                },
                    CustomerEmail = user.Email,
                    Metadata = new Dictionary<string, string> { { "OrderNumber", order.OrderNumber } },
                    Mode = "payment"
                };

                var service = await _sessionService.CreateAsync(options);
                return service.Url;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
