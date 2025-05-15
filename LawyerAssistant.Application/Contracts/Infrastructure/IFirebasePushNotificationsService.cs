namespace LawyerAssistant.Application.Contracts.Infrastructure;

public interface IFirebasePushNotificationsService
{
    Task SendToAll(string title, string messageText, string routePath = null);
    Task SendToSpecialUser(string title, string messageText, List<string> _fcmToken, string routePath = null);
    Task SubscribeToTopic(List<string> fcmTokens);
}