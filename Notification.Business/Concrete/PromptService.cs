using Notification.Business.Abstract;
using Notification.Business.Repository.Abstract;
using Notification.Entities;
using Notification.Entities.Enum;
using Notification.Service.Common.Service.Notifier.IOSNotifier;
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
            var notifications = await _notificationService.PrepareNotifications(notificationRequest);

            foreach (var notification in notifications)
            {
                await _notificationInfoRepository.Add(notification);

                var notifierSender = notifierSenderFactory(notification.Platform);

                if (notifierSender != null)
                {
                    await Task.Run(() => { notifierSender.Notify(notification); });
                }
            }
        }

        private INotifier notifierSenderFactory(NotificationPlatform notificationPlatform)
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