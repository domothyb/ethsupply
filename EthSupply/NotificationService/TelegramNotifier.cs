namespace EthSupply.NotificationService;

public class TelegramNotifier(string token, long channelId) : INotificationService
{
    public async Task Notify(string message)
    {
        var data = new Dictionary<string, string>
        {
            { "chat_id", channelId.ToString() },
            { "text", message }
        };
        
        using HttpClient client = new HttpClient();
        var content = new FormUrlEncodedContent(data);
        var response = await client.PostAsync($"https://api.telegram.org/bot{token}/sendMessage", content);
        response.EnsureSuccessStatusCode();
    }
}