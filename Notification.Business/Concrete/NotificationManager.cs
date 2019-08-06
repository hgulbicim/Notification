using Notification.Business.Abstract;
using Notification.Business.Repository.Abstract;
using Notification.Entities;
using Notification.Entities.Enum;
using Nustache.Core;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Notification.Business.Service
{
    public class NotificationManager : INotificationService
    {
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly INotificationInfoRepository _notificationInfoRepository;

        public NotificationManager(INotificationTemplateService notificationTemplateService, INotificationInfoRepository notificationInfoRepository)
        {
            _notificationTemplateService = notificationTemplateService;
            _notificationInfoRepository = notificationInfoRepository;
        }

        public async Task<NotificationInfo> Inquiry(InquiryRequest inquiryRequest)
        {
            return await _notificationInfoRepository.Get(x => x.RequestId == inquiryRequest.RequestId);
        }

        public async Task<List<NotificationInfo>> Prepare(NotificationRequest notificationRequest)
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
                    Status = NotificationStatus.Waiting,
                    RequestId = notificationRequest.RequestInfo.RequestId
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