using EthSupply.DataRepository;
using EthSupply.NotificationService;
using EthSupply.SupplyFetcher;

namespace EthSupply;

public class Program
{
    private readonly INotificationService notificationService;
    private readonly ISupplyFetcher supplyFetcher;
    private readonly IDataRepository dataRepository;

    public Program(INotificationService notificationService, ISupplyFetcher supplyFetcher, IDataRepository dataRepository)
    {
        this.notificationService = notificationService;
        this.supplyFetcher = supplyFetcher;
        this.dataRepository = dataRepository;
    }

    public async Task Run()
    {
        // Todo
    }

    public static async Task Main(string[] args)
    {
        var dataFilePath = args[0];
        var dataRepository = new JsonRepository(dataFilePath);
        var notificationService = new TelegramNotifier(dataRepository.GetBotToken(), dataRepository.GetChannelId());
        var supplyFetcher = new UltrasoundSupplyFetcher();

        var program = new Program(notificationService, supplyFetcher, dataRepository);
        await program.Run();
    }
}