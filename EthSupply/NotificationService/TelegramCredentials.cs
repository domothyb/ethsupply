using Newtonsoft.Json;

namespace EthSupply.NotificationService;

public class TelegramCredentials
{
    [JsonProperty("token")] public string Token { get; set; } = null!;

    [JsonProperty("channelId")] public long ChannelId { get; set; }
}