using Notification.Entities;
using System;
using System.Threading.Tasks;

namespace Notification.Business.Abstract
{
    public interface INotificationTemplateService
    {
        Task<NotificationTemplate> Find(Func<NotificationTemplate, bool> filter);
    }
}