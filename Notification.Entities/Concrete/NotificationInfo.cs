using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Notification.Entities.Common;
using Notification.Entities.Enum;
using System;

namespace Notification.Entities
{
    public class NotificationInfo : IEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public NotificationPlatform Platform { get; set; }
        //Email ise e-posta adresi
        //IOS ise Notification Key
        //Android ise Registration Key
        //SMS ise telefon numarası
        public string Recipient { get; set; }
        public string Language { get; set; }
        public NotificationStatus Status { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime ScheduleDate { get; set; } = DateTime.Now;
    }
}
