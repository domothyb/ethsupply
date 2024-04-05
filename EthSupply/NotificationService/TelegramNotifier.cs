using Newtonsoft.Json;

namespace EthSupply.NotificationService;

public class TelegramNotifier : INotificationService
{
    private readonly string token;
    private readonly long channelId;

    private TelegramNotifier(string token, long channelId)
    {
        this.token = token;
        this.channelId = channelId;
    }

    public static TelegramNotifier Load(string dataFilePath)
    {
        var jsonContent = File.ReadAllText(dataFilePath);
        var credentials = JsonConvert.DeserializeObject<TelegramCredentials>(jsonContent) ?? throw new InvalidOperationException();

        return new TelegramNotifier(credentials.Token, credentials.ChannelId);
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

    public async Task AlertIncrease(long supply)
    {
        await Notify("Hello up");
    }

    public async Task AlertDecrease(long supply)
    {
        await Notify("Hello down");
    }
}