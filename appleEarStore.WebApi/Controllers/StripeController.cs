using Data.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Newtonsoft.Json;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StripeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;

        public StripeController(IConfiguration configuration, IOrderService orderService)
        {
            _configuration = configuration;
            _orderService =  orderService;
        }

        [HttpPost]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = Stripe.EventUtility.ParseEvent(json, false);
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    if(session.PaymentStatus == "paid" && session.Metadata.ContainsKey("OrderNumber"))
                    {
                        var orderNumber = session.Metadata["OrderNumber"];
                        if(orderNumber != null)
                        {
                            await _orderService.PaidOrder(orderNumber);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                if (stripeEvent.Type == Events.CheckoutSessionExpired)
                {
                    var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    if (session.PaymentStatus == "paid" && session.Metadata.ContainsKey("OrderNumber"))
                    {
                        var orderNumber = session.Metadata["OrderNumber"];
                        if (orderNumber != null)
                        {
                            await _orderService.RemoveExpiredOrder(orderNumber);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e);
            }
        }
    }
}
