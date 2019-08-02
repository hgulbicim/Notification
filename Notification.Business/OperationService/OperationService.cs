using Notification.Business.Abstract;
using Notification.Contract.Abstract;
using Notification.Entities;
using Notification.Entities.Common;
using Notification.Entities.Concrete;
using System.Threading.Tasks;

namespace Notification.Business.OperationService
{
    public class OperationService : IOperationService
    {
        private readonly IPromptService _promptService;

        public OperationService(IPromptService promptService)
        {
            _promptService = promptService;
        }

        public async Task<NotificationResponse> AddNotification(NotificationRequest notificationRequest)
        {
            await _promptService.SendNotification(notificationRequest);

            return new NotificationResponse() { ResponseInfo = new ResponseInfo { Code = "00", Message = "Success", Status = true } };
        }
    }
}