using Data.DTO.Order;
using Data.DTO.Pagination;
using Data.DTO.Product;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;
using System.Collections.Generic;

namespace Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
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

        public async Task<Order> CreateOrder(CreateOrder order, int userId)
        {   
            try
            {
                User user = await _userRepository.GetById(userId) ?? throw new Exception("Utilisateur non trouvé. Vous ne pouvez pas faire aboutir cette commande.");
                //Verification du solde client.
                if (user.Balance < order.UsedBalance)
                {
                    throw new Exception("Votre solde ne contient pas ce montant veuillez le réajuster.");
                }
                // Chercher l'adresse et voir si ell existe et appartient bien a l'utilisateur.
                // Address address = await _addressRepository.FindBy(a => a.Id == order.AddressId && a.UserId == userId) ?? throw new Exception("Cette adresse n'existe pas.");
                Address address = new Address()
                {
                    UserId = user.Id,
                    Name = "Mon Adresse",
                    Street = "74 rue francis de DJO",
                    PhoneNumber = "0685995845",
                    City = "Villeurbanne",
                    PostalCode = "69100",
                    Country = "France",
                    CreatedAt = DateTime.Now,
                };
                decimal totalWithoutTax = 0;
                decimal total = 0;
                decimal totalWeight = 0;

                //Calcul les prix total des produits.
                foreach (ProductOrderCreate poc in order.Products)
                {
                    Product product = await _productRepository.GetById(poc.ProductId);
                    // PromoCode promoCode = _promoRepository.FindBy(pc => pc.Id == poc.PromoCodeId && si une des categories a le meme Id que product.CategoriesId)
                    if (product != null)
                    {
                        totalWithoutTax += product.PriceWithoutTax * poc.Quantity;
                        total += product.Price * poc.Quantity;
                        totalWeight += product.Weight * poc.Quantity;
                    }

                }
                user.Balance = (user.Balance - order.UsedBalance);

                Order orderToCreate = new Order()
                {
                    UserId = userId,
                    Total_without_tax = totalWithoutTax,
                    Total = (total - order.UsedBalance),
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

                Order createdOrder = await _orderRepository.Insert(orderToCreate);
                User updateUser = await _userRepository.Update(user);
                return createdOrder;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
