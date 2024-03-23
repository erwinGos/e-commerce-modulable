using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Data.DTO.User;
using Data.Managers;
using Data;
using Database;
using Data.Repository.Contract;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("getAll")]
        [Authorize()]
        public async Task<IActionResult> GetNotificationList()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);
                List<Notifications> notifications = await _notificationService.GetNotificationsList(userId);
                return Ok(notifications);
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
