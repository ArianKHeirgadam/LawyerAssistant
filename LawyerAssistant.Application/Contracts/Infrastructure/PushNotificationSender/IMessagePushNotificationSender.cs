using Application.Enums;

namespace LawyerAssistant.Application.Contracts.Infrastructure.PushNotificationSender;

public interface IMessagePushNotificationSender
{
    Task Send(int userId, string Message, int? relatedId, NotificationMessageType notificationMessageType);
}