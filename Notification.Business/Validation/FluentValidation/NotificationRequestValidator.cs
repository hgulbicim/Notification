using FluentValidation;
using Notification.Entities;
using System;

namespace Notification.Business.Validation.FluentValidation
{
    public class NotificationRequestValidator : AbstractValidator<NotificationRequest>
    {
        public NotificationRequestValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(m => m.ChannelCode).NotEmpty();

            RuleFor(m => m.Language).NotEmpty();

            RuleFor(m => m.TemplateItem).NotEmpty();

            RuleFor(m => m.ScheduleDate).GreaterThanOrEqualTo(p => DateTime.Now.AddMinutes(-1));

            RuleFor(m => m.TemplateId).NotEmpty();

            RuleFor(m => m.Recipients).NotEmpty();

            RuleFor(m => m.Recipients).NotEmpty();
        }
    }
}