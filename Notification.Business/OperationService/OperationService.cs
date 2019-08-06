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
        private readonly INotificationService _notificationService;

        public OperationService(IPromptService promptService, INotificationService notificationService)
        {
            _promptService = promptService;
            _notificationService = notificationService;
        }

        public async Task<NotificationResponse> AddNotification(NotificationRequest notificationRequest)
        {
            await _promptService.SendNotification(notificationRequest);

            return new NotificationResponse() { ResponseInfo = new ResponseInfo { Code = "00", Message = "Success", Status = true } };
        }

        public async Task<InquiryResponse> InquiryNotification(InquiryRequest inquiryRequest)
        {
            var inquiry = await _notificationService.Inquiry(inquiryRequest);

            return new InquiryResponse()
            {
                DeliveryDate = inquiry?.DeliveryDate,
                Status = inquiry?.Status,
                ResponseInfo = new ResponseInfo { Code = "00", Message = "Success", Status = true }
            };
        }
    }
}