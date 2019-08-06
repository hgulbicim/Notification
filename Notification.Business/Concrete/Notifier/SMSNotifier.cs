﻿using Notification.Business.Abstract;
using Notification.Entities;

namespace Notification.Service.Common.Service.Notifier
{
    public class SMSNotifier : INotifierService
    {
        public void Notify(NotificationInfo notificationInfo)
        {
            System.Console.WriteLine($"{notificationInfo.Subject} {notificationInfo.Platform.GetHashCode().ToString()}");
        }
    }
}