using Database;

namespace Data.Repository.Contract;
public interface INotificationService
{
    public Task<List<Notifications>> GetNotificationsList(int userId);
}
