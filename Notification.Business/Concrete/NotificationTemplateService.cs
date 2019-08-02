using Notification.Business.Abstract;
using Notification.Entities;
using Notification.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Service.Common.Service
{
    public class NotificationTemplateManager : INotificationTemplateService
    {
        private List<NotificationTemplate> notificationTemplateList { get; set; } = new List<NotificationTemplate>();

        public NotificationTemplateManager()
        {
            NotificationTemplate notificationTemplateFirst = new NotificationTemplate
            {
                Id = "ConfirmMail",
                Language = "en",
                Platforms = new List<NotificationPlatform>
                {
                     NotificationPlatform.Email
                },
                Message = "Dear {{Fullname}}, Confirm Mail Link: {{CallbackUrl}}",
                Subject = "Dear {{Fullname}}, confirm mail",
                TemplateId = "ConfirmMail",
                FilePath = "template/html/confirmmail/en/template.html",
                TemplateType = TemplateType.File
            };

            NotificationTemplate notificationTemplateSecond = new NotificationTemplate
            {
                Id = "ConfirmMail",
                Language = "en",
                Message = "Dear {{Fullname}}, Confirm Mail Link: {{CallbackUrl}}",
                Platforms = new List<NotificationPlatform>
                {
                     NotificationPlatform.SMS
                },
                Subject = "Dear {{Fullname}}, confirm mail",
                TemplateId = "ConfirmMail",
                TemplateType = TemplateType.Text
            };

            notificationTemplateList.Add(notificationTemplateFirst);

            notificationTemplateList.Add(notificationTemplateSecond);
        }

        public async Task<NotificationTemplate> Find(Func<NotificationTemplate, bool> filter)
        {
            await Task.Delay(0);

            //TODO: read mongodb

            return notificationTemplateList.FirstOrDefault(filter);
        }
    }
}
