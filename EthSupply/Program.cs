using System.Reflection;
using EthSupply.DataRepository;
using EthSupply.NotificationService;
using EthSupply.SupplyFetcher;

namespace EthSupply;

class Program
{
    public static async Task Main(string[] args)
    {
        var dataFilePath = args[0];
        IDataRepository repo = new JsonRepository(dataFilePath);
        var token = repo.GetBotToken();
        var channelId = repo.GetChannelId();

        repo.SetLastSupplyAlert(1.5);
        
        //var notifier = new TelegramNotifier(token, channelId);
        //await notifier.Notify("hello");
    }
}