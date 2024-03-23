using Data.Repository.Contract;
using Database;

namespace Data.Repository.Contract;
public interface INotificationRepository : IGenericRepository<Notifications>
{
    Task<List<Notifications>> GetNotificationList(int userId);
}