using Notification.Business.Abstract;
using Notification.Entities;
using Notification.Entities.Enum;
using Nustache.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Notification.Business.Service
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationTemplateService _notificationTemplateService;
        public NotificationManager(INotificationTemplateService notificationTemplateService)
        {
            _notificationTemplateService = notificationTemplateService;
        }

        public async Task<List<NotificationInfo>> PrepareNotifications(NotificationRequest notificationRequest)
        {
            List<NotificationInfo> notifications = new List<NotificationInfo>();

            foreach (var notificationRecipients in notificationRequest.Recipients)
            {
                var notificationTemplate = await _notificationTemplateService.Find(p => p.TemplateId == notificationRequest.TemplateId &&
                                                                      p.Language == notificationRequest.Language &&
                                                                      p.Platforms.Contains(notificationRecipients.Platform));

                if (notificationTemplate == null)
                {
                    continue;
                    //throw new Exception($"Unable to find '{dto.TemplateId}' template in '{dto.Language}' language for '{platform.ToString()}' platform");
                }

                string message = prepareMessage(notificationTemplate, notificationRequest.TemplateItem);
                string subject = Render.StringToString(notificationTemplate.Subject, notificationRequest.TemplateItem);

                var notification = new NotificationInfo
                {
                    ScheduleDate = notificationRequest.ScheduleDate,
                    Language = notificationRequest.Language,
                    Message = message,
                    Subject = subject,
                    Platform = notificationRecipients.Platform,
                    Recipient = notificationRecipients.Recipient,
                    DeliveryDate = DateTime.Now,
                    Status = NotificationStatus.Waiting
                };

                notifications.Add(notification);
            }

            return notifications;
        }

        private string prepareMessage(NotificationTemplate notificationTemplate, dynamic templateItem)
        {
            switch (notificationTemplate.TemplateType)
            {
                case TemplateType.Text:
                    return Render.StringToString(notificationTemplate.Message, templateItem);
                case TemplateType.File:
                    if (File.Exists(notificationTemplate.FilePath))
                    {
                        return Render.FileToString(notificationTemplate.FilePath, templateItem);
                    }
                    return string.Empty;
                default:
                    return string.Empty;
            }
        }
    }
}