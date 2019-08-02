using Notification.Entities.Enum;
using System.Collections.Generic;

namespace Notification.Entities
{
    public class NotificationTemplate
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string TemplateId { get; set; }
        public string FilePath { get; set; }
        public string Language { get; set; }
        public List<NotificationPlatform> Platforms { get; set; }
        public TemplateType TemplateType { get; set; }
    }
}