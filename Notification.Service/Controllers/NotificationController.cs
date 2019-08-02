using Microsoft.AspNetCore.Mvc;
using Notification.Contract.Abstract;
using Notification.Entities;
using System.Threading.Tasks;

namespace Notification.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotificationController : ControllerBase
    {
        private readonly IOperationService _operationService;
        public NotificationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(NotificationRequest notificationRequest)
        {
            return Ok(await _operationService.AddNotification(notificationRequest));
        }
    }
}