using Data.DTO.Order;
using Data.DTO.Pagination;
using Data.DTO.ProductDto;
using Data.Managers;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;
using AutoMapper;

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
        private readonly IMapper _mapper;
        private readonly OrderManager _orderManager;
        private readonly IStripeService _stripeService;

        public OrderService(IStripeService stripeService, OrderManager orderManager, IMapper mapper, IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository, IPromoRepository promoRepository, IProductOrderRepository productOrderRepository, IAddressRepository addressRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _promoRepository = promoRepository;
            _productRepository = productRepository;
            _productOrderRepository = productOrderRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
            _orderManager = orderManager;
            _stripeService = stripeService;
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

        public async Task<OrderRead> CreateOrder(CreateOrder createOrder, int userId)
        {
            decimal totalWithoutTax = 0;
            decimal Discount_amount = 0;
            decimal total = 0;
            decimal totalWeight = 0;
            List<ProductOrder> productOrderList = [];
            List<PromoCode> UsedPromoCodes = []; 
            try
            {
                // Rechercher l'adresse de l'utilisateur. if null throw ERROR
                Address address = await _addressRepository.FindSingleBy(address => address.Id == createOrder.AddressId && address.UserId == userId) ?? throw new Exception("Cette addresse n'est pas valide. Veuillez reessayer une addresse différente."); // A SUPPRIMER A LA FIN !!

                // Rechercher l'utilisateur. if null throw ERROR
                User user = await _userRepository.FindSingleBy(user => user.Id == userId, user => user.PromoCodes) ?? throw new Exception("Utilisateur non trouvé.");
                
                //Verifications : PromoCode expiration, UnpaidOrders, Verifie le solde utilisateur et PromoCode alreadyUsed if needed
                await _orderManager.CheckForOrder(user, createOrder);

                foreach (ProductOrderCreate poc in createOrder.Products)
                {
                    Product product = await _productRepository.FindSingleBy(product => product.Id == poc.ProductId, product => product.PromoCodes);
                    if(poc.Quantity > product.CurrentStock)
                    {
                        throw new Exception("Un ou plusieurs produits ne sont plus en stock dans les quantités indiquées, veuillez verifier votre panier.");
                    }
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


                        Product copyProduct = product;
                        copyProduct.CurrentStock -= poc.Quantity;
                        await _productRepository.Update(copyProduct);

                        // With Tax
                        if (product.PromoCodes.Count > 0)
                        {
                            var highestDiscountPromoCode = product.PromoCodes
                            .OrderByDescending(promo => promo.DiscountPercentage)
                            .Where(promo => createOrder.PromoCode.Any(code => code == promo.Code))
                            .FirstOrDefault();
                            if (highestDiscountPromoCode != null)
                            {
                                decimal TotalPoWithoutDiscount = product.Price * poc.Quantity;
                                decimal ProductPriceAfterDiscount = product.Price * (1 - highestDiscountPromoCode.DiscountPercentage);
                                decimal TotalPo = ProductPriceAfterDiscount * poc.Quantity;
                                total += TotalPo;
                                Discount_amount += TotalPoWithoutDiscount - TotalPo;
                                productOrder.Total = TotalPo;
                                productOrder.UsedPromoCode = highestDiscountPromoCode.Code;
                                productOrder.Discount_Amount = TotalPoWithoutDiscount - TotalPo;
                                // Ajout du promocode utilisé a la liste afin de le rendre inutilisable la prochaine fois.
                                if(highestDiscountPromoCode.SingleTimeUsage)
                                {
                                    user.PromoCodes.Add(highestDiscountPromoCode);
                                }
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
                var orderNumber = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                Order orderToCreate = new()
                {
                    UserId = userId,
                    Total_without_tax = totalWithoutTax,
                    Discount_amount = Discount_amount,
                    Total = (total - createOrder.UsedBalance),
                    TotalWeight = totalWeight,
                    OrderNumber = orderNumber,
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
                string StripePaymentUrl = await _stripeService.CreatePaymentLink(orderToCreate, user);
                orderToCreate.StripePaymentUrl = StripePaymentUrl;
                Order createdOrder = await _orderRepository.Insert(orderToCreate);
                foreach(ProductOrder productOrder in productOrderList)
                {
                    productOrder.OrderId = createdOrder.Id;
                    await _productOrderRepository.Insert(productOrder);
                }

                User updateUser = await _userRepository.Update(user);

                return _mapper.Map<OrderRead>(createdOrder);

            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> PaidOrder(string orderNumber)
        {
            try
            {
                return await _orderRepository.ChangeToPaidOrder(orderNumber);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> RemoveExpiredOrder(string orderNumber)
        {
            try
            {
                return await _orderRepository.RemoveExpiredOrder(orderNumber);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
