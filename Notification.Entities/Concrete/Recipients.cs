using Notification.Entities.Enum;

namespace Notification.Entities.Concrete
{
    public class Recipients
    {
        public string Recipient { get; set; }
        public NotificationPlatform Platform { get; set; }
    }
}
