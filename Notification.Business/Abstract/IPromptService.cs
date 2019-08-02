using Notification.Entities;
using System.Threading.Tasks;

namespace Notification.Business.Abstract
{
    public interface IPromptService
    {
        Task SendNotification(NotificationRequest notificationRequest);
    }
}