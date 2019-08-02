using Notification.Entities;

namespace Notification.Business.Abstract
{
    public interface INotifier
    {
        void Notify(NotificationInfo notificationInfo);
    }
}
