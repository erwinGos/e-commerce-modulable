using Data.DTO.Order;
using Data.Repository.Contract;
using Database.Entities;


namespace Data.Managers
{
    public class OrderManager
    {

        private readonly IPromoRepository _promoRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderManager(IPromoRepository promoRepository, IOrderRepository orderRepository)
        {
            _promoRepository = promoRepository;
            _orderRepository = orderRepository;
        }


        public static Order CreateOrderInstance(int userId, decimal totalWithoutTax, decimal total, decimal totalWeight, CreateOrder createOrder, Address address)
        {
            Order order = new()
            {
                UserId = userId,
                Total_without_tax = totalWithoutTax,
                Total = (total - createOrder.UsedBalance),
                TotalWeight = totalWeight,
                OrderNumber = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(),
                Street = address.Street,
                PhoneNumber = address.PhoneNumber,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country,
                ParcelTracking = "NONE",
                EstimatedDeliveryDate = DateTime.Now, // c'est le transporteur qui le retourne lors de la création de du ticket qui sera fait quand lacommande sera payé.
                DeliveryDate = DateTime.Now, // a mettre en non required parce que c'est le transporteur qui le retourne ça.
                CreatedAt = DateTime.Now
            };
            return order;
        }

        public async Task<bool> CheckForOrder(User user, CreateOrder createOrder)
        {
            try
            {
                if (user.Balance < createOrder.UsedBalance)
                {
                    throw new Exception("Votre solde est inférieur à la somme renseignée. Veuillez la réajuster.");
                }

                //Verifie si le code est déjà utilisé
                List<PromoCode> checkIfAlreadyUsedPromoCodes = user.PromoCodes.Where(promo => createOrder.PromoCode.Any(plannedToBeUsedPromo => plannedToBeUsedPromo == promo.Code)).ToList();
                if (checkIfAlreadyUsedPromoCodes.Count > 0)
                {
                    throw new Exception("Vous avez déjà utilisé le(s) code(s) promotionnel(s) suivant(s) : " + string.Join(", ", checkIfAlreadyUsedPromoCodes.Select(promo => promo.Code)));
                }

                // Verifie si l'utilsateur a déjà des commandes non payés. if !null throw ERROR
                List<Order> checkIfHasUnpaidOrder = await _orderRepository.FindBy(order => order.UserId == user.Id && order.HasBeenPaid == false);
                if (checkIfHasUnpaidOrder.Count > 0)
                {
                    throw new Exception("Vous ne pouvez pas passer commande. Veuillez Payer vos commandes déjà dues.");
                }

                //Verifie si le code est expiré
                List<PromoCode> RequestedPromoCodes = await _promoRepository.FindBy(promo => createOrder.PromoCode.Any(code => code == promo.Code));
                IEnumerable<PromoCode> CheckIfPromoIsExpired = RequestedPromoCodes.Where(promo => promo.ExpirationDate < DateTime.Now);

                if (CheckIfPromoIsExpired.Count() > 0)
                {
                    throw new Exception("Le(s) code(s) promotionnel(s) suivant(s) est / sont expiré(s): " + string.Join(", ", CheckIfPromoIsExpired.Select(promo => promo.Code)));
                }

                return true;

            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
