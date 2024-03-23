using Data.Repository.Contract;
using Database;

namespace Data.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository) 
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<List<Notifications>> GetNotificationsList(int userId) 
    {
        try 
        {
            List<Notifications> notifications = await _notificationRepository.GetNotificationList(userId);
            return notifications;
        } catch(Exception ex) 
        {
            throw new Exception(ex.Message);
        }
    }
}
