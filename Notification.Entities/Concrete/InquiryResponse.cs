using Notification.Entities.Common;
using Notification.Entities.Enum;
using System;

namespace Notification.Entities.Concrete
{
    public class InquiryResponse : ResponseBase
    {
        public NotificationStatus? Status { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}