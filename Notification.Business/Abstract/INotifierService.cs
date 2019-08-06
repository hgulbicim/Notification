using Notification.Entities;

namespace Notification.Business.Abstract
{
    public interface INotifierService
    {
        void Notify(NotificationInfo notificationInfo);
    }
}