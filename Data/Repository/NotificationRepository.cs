using Data.Repository;
using Data.Repository.Contract;
using Database;

namespace Data.Repository;
public class NotificationRepository : GenericRepository<Notifications>, INotificationRepository
{
    public NotificationRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<List<Notifications>> GetNotificationList(int userId)
    {
        try
        {
            var query = _table
                .Take(10);

            var result = query.Where(notification => notification.UserId == userId).ToList();
            return result;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
}
