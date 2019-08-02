using Notification.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notification.Business.Abstract
{
    public interface INotificationService
    {
        Task<List<NotificationInfo>> PrepareNotifications(NotificationRequest notificationRequest);
    }
}