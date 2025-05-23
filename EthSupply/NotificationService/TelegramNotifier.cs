﻿using Newtonsoft.Json;

namespace EthSupply.NotificationService;

public class TelegramNotifier : INotificationService
{
    private const string DECREASE_INDICATOR = "🔥";
    private const string INCREASE_INDICATOR = "💧";
    private readonly long channelId;

    private readonly string token;

    private TelegramNotifier(string token, long channelId)
    {
        this.token = token;
        this.channelId = channelId;
    }

    public async Task AlertIncrease(long supply)
    {
        await Notify($"{INCREASE_INDICATOR} {supply:N0}");
    }

    public async Task AlertDecrease(long supply)
    {
        await Notify($"{DECREASE_INDICATOR} {supply:N0}");
    }

    public static TelegramNotifier Load(string dataFilePath)
    {
        var jsonContent = File.ReadAllText(dataFilePath);
        var credentials = JsonConvert.DeserializeObject<TelegramCredentials>(jsonContent) ??
                          throw new InvalidOperationException();

        return new TelegramNotifier(credentials.Token, credentials.ChannelId);
    }

    private async Task Notify(string message)
    {
        var url = $"https://api.telegram.org/bot{token}/sendMessage";
        var data = new Dictionary<string, string>
        {
            { "chat_id", channelId.ToString() },
            { "text", message }
        };

        using var client = new HttpClient();
        var content = new FormUrlEncodedContent(data);
        var response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
    }
}