namespace EthSupply.NotificationService;

public class TelegramNotifier : INotificationService
{
    private readonly string token;
    private readonly long channelId;

    public TelegramNotifier(string token, long channelId)
    {
        this.token = token;
        this.channelId = channelId;
    }

    private async Task Notify(string message)
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

    public void AlertIncrease(long supply)
    {
        throw new NotImplementedException();
    }

    public void AlertDecrease(long supply)
    {
        throw new NotImplementedException();
    }
}