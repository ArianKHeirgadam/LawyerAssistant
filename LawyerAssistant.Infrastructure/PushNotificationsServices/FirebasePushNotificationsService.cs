/*using Application.Contracts.Common;
using Application.Contracts.Infrastructure;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
namespace Common.Service;

public class FirebasePushNotificationsService : IFirebasePushNotificationsService, IScoped
{
    public FirebasePushNotificationsService()
    {
        var instance = FirebaseApp.DefaultInstance;
        if (instance == null)
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Directory.GetCurrentDirectory() + "\\firebaseConfig.json"),
            });
    }

    public async Task SendToAll(string title, string messageText, string routePath = null)
    {
        Dictionary<string, string> data = null;

        if (!string.IsNullOrWhiteSpace(routePath))
            data = new Dictionary<string, string>() { { "routePath", routePath } };

        var message = new Message()
        {
            Topic = "public",
            Notification = new Notification()
            {
                Body = messageText,
                Title = title
            },
            Data = data
        };
        var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
    }

    public async Task SendToSpecialUser(string title, string messageText, List<string> _fcmTokens, string routePath = null)
    {
        Dictionary<string, string> data = null;

        if (!string.IsNullOrWhiteSpace(routePath))
            data = new Dictionary<string, string>() { { "routePath", routePath } };


        var deviceIds = _fcmTokens.Where(c => !string.IsNullOrEmpty(c)).ToList();
        if (!deviceIds.Any())
            return;
        var message = new MulticastMessage()
        {
            Tokens = deviceIds,
            Notification = new Notification()
            {
                Body = messageText,
                Title = title,
            },
            Data = data
        };
        var response = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message);
        int a = 0;
    }


    public async Task SubscribeToTopic(List<string> fcmTokens)
    {
        IReadOnlyList<string> data = fcmTokens;
        _ = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(data, "public");
    }
}*/