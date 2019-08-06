using Notification.Entities;
using Notification.Entities.Concrete;
using System.Threading.Tasks;

namespace Notification.Contract.Abstract
{
    public interface IOperationService
    {
        Task<NotificationResponse> AddNotification(NotificationRequest notificationRequest);

        Task<InquiryResponse> InquiryNotification(InquiryRequest inquiryRequest);
    }
}