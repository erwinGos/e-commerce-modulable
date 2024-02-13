using Data.DTO.Order;
using Data.DTO.Pagination;
using Data.DTO.Product;
using Data.Managers;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPromoRepository _promoRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IAddressRepository _addressRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository, IPromoRepository promoRepository, IProductOrderRepository productOrderRepository, IAddressRepository addressRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _promoRepository = promoRepository;
            _productRepository = productRepository;
            _productOrderRepository = productOrderRepository;
            _addressRepository = addressRepository;
        }

        public async Task<Order> GetSingleOrder(int orderId, int userId, PaginationParameters parameters)
        {
            try
            {
                Order order = await _orderRepository.FindSingleBy(o => o.Id == orderId && (o.UserId == userId || parameters.isAdmin)) ?? throw new Exception("Cette commande n'existe pas ou ne vous appartient pas.");
                return order;
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Order>> GetUserOrderList(int UserId, PaginationParameters parameters)
        {
            try
            {
                List<Order> orders = await _orderRepository.GetOrderListAsync(UserId, parameters);
                return orders;
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Order>> GetAllUsersOrders(PaginationParameters parameters)
        {
            try
            {
                List<Order> orders = await _orderRepository.GetAllOrdersPagination(parameters);
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> CreateOrder(CreateOrder createOrder, int userId)
        {
            decimal totalWithoutTax = 0;
            decimal total = 0;
            decimal totalWeight = 0;
            List<ProductOrder> productOrderList = [];
            try
            {
                // Rechercher l'utilisateur. if null throw ERROR
                User user = await _userRepository.FindSingleBy(user => user.Id == userId, user => user.PromoCodes) ?? throw new Exception("Utilisateur non trouvé.");
                
                // Verifie si l'utilsateur a déjà des commandes non payés. if !null throw ERROR
                Order checkIfHasUnpaidOrder = await _orderRepository.FindSingleBy(order => order.UserId == userId && order.HasBeenPaid == false) != null ? throw new Exception("Vous ne pouvez pas passer commande. Veuillez Payer vos commandes déjà dues.") : null;
                
                // Rechercher l'adresse de l'utilisateur. if null throw ERROR
                Address address = await _addressRepository.FindSingleBy(address => address.Id == createOrder.AddressId && address.UserId == userId) ?? throw new Exception("Cette addresse n'est pas valide. Veuillez reessayer une addresse différente."); // A SUPPRIMER A LA FIN !!
                
                // Verifie si la somme utilisé lors de la commande est inférieur au solde de l'utilsateur.
                if (user.Balance < createOrder.UsedBalance)
                {
                    throw new Exception("Votre solde est inférieur à la somme renseignée. Veuillez la réajuster.");
                }

                foreach (ProductOrderCreate poc in createOrder.Products)
                {
                    Product product = await _productRepository.FindSingleBy(product => product.Id == poc.ProductId, product => product.PromoCodes);
                    ProductOrder productOrder = new ProductOrder()
                    {
                        ProductId = poc.ProductId,
                        Quantity = poc.Quantity,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    if (product != null)
                    {
                        totalWeight += product.Weight * poc.Quantity;
                        //Without Tax
                        decimal totalWithoutTaxPo = product.PriceWithoutTax * poc.Quantity;
                        totalWithoutTax += totalWithoutTaxPo;
                        productOrder.TotalWithoutTax = totalWithoutTaxPo;

                        // With Tax
                        if(product.PromoCodes.Count > 0)
                        {
                            var highestDiscountPromoCode = product.PromoCodes
                                .Where(promo => createOrder.PromoCode.Contains(promo.Code))
                                .OrderByDescending(promo => promo.DiscountPercentage)
                                .FirstOrDefault();
                            if (highestDiscountPromoCode != null)
                            {
                                decimal ProductPriceAfterDiscount = product.Price * (1 - highestDiscountPromoCode.DiscountPercentage);
                                decimal TotalPo = ProductPriceAfterDiscount * poc.Quantity;
                                total += TotalPo;
                                productOrder.Total = TotalPo;
                                productOrder.UsedPromoCode = highestDiscountPromoCode.Code;
                            } else
                            {
                                decimal TotalPo = product.Price * poc.Quantity;
                                total += TotalPo;
                                productOrder.Total = TotalPo;
                            }
                        } else
                        {
                            decimal TotalPo = product.Price * poc.Quantity;
                            total += TotalPo;
                            productOrder.Total = TotalPo;
                        }

                        //Ajout à la liste des productOrder pour les créer ensuite.
                        productOrderList.Add(productOrder);
                        
                    }

                }
                if(createOrder.UsedBalance > total)
                {
                    throw new Exception("L'utilisation du solde ne peut pas être supérieure au montant de la commande.");
                }
                user.Balance -= createOrder.UsedBalance;
                Order orderToCreate = new()
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
                    ParcelTracking = "NONE", // Implementation transporteur.
                    EstimatedDeliveryDate = DateTime.Now, // Implementation transporteur.
                    DeliveryDate = DateTime.Now, // Implementation transporteur.
                    CreatedAt = DateTime.Now
                };

                Order createdOrder = await _orderRepository.Insert(orderToCreate);
                foreach(ProductOrder productOrder in productOrderList)
                {
                    productOrder.OrderId = createdOrder.Id;
                    await _productOrderRepository.Insert(productOrder);
                }
                User updateUser = await _userRepository.Update(user);

                return createdOrder;

            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
