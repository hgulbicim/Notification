using Notification.Entities.Common;
using Notification.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Notification.Entities
{
    public class NotificationRequest : RequestBase
    {
        public string ChannelCode { get; set; }
        public DateTime ScheduleDate { get; set; } = DateTime.Now;
        public string Language { get; set; }
        public string TemplateId { get; set; }
        public List<Recipients> Recipients { get; set; }
        public dynamic TemplateItem { get; set; }
    }
}