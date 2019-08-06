using Hangfire;
using Notification.Business.Abstract;
using Notification.Business.Repository.Abstract;
using Notification.Entities;
using Notification.Entities.Enum;
using Notification.Service.Common.Service.Notifier;
using System;
using System.Threading.Tasks;

namespace Notification.Service.Common.Service
{
    public class PromptManager : IPromptService
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationInfoRepository _notificationInfoRepository;

        public PromptManager(INotificationService notificationService, INotificationInfoRepository notificationInfoRepository)
        {
            _notificationService = notificationService;
            _notificationInfoRepository = notificationInfoRepository;
        }

        public async Task SendNotification(NotificationRequest notificationRequest)
        {
            var notifications = await _notificationService.Prepare(notificationRequest);

            foreach (var notification in notifications)
            {
                var entity = await _notificationInfoRepository.Add(notification);

                var notifierSender = notifierSenderFactory(notification.Platform);

                if (notifierSender != null)
                {
                    if (notification.ScheduleDate.HasValue == false || (notification.ScheduleDate.HasValue &&
                                                                        notification.ScheduleDate.Value.Subtract(DateTime.Now).TotalMinutes >= -1 &&
                                                                        notification.ScheduleDate.Value.Subtract(DateTime.Now).TotalMinutes <= 5))
                    {
                        await Task.Run(() =>
                        {
                            notifierSender.Notify(notification);
                        });
                    }
                    else
                    {
                        await Task.Run(() =>
                        {
                            var jobId = BackgroundJob.Schedule(() => notifierSender.Notify(notification), notification.ScheduleDate.Value);
                        });
                    }
                }
            }
        }

        private INotifierService notifierSenderFactory(NotificationPlatform notificationPlatform)
        {
            switch (notificationPlatform)
            {
                case NotificationPlatform.iOS:
                    return new IOSNotifier();
                case NotificationPlatform.Android:
                    return new AndroidNotifier();
                case NotificationPlatform.Email:
                    return new EmailNotifier();
                case NotificationPlatform.SMS:
                    return new SMSNotifier();
                case NotificationPlatform.Web:
                    return new WebNotifier();
                default:
                    return null;
            }
        }
    }
}