using Notification.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notification.Business.Abstract
{
    public interface INotificationService
    {
        Task<List<NotificationInfo>> Prepare(NotificationRequest notificationRequest);

        Task<NotificationInfo> Inquiry(InquiryRequest inquiryRequest);
    }
}