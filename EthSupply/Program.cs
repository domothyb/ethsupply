using System.Globalization;
using EthSupply.DataRepository;
using EthSupply.NotificationService;
using EthSupply.SupplyService;
using Newtonsoft.Json;

namespace EthSupply;

public partial class Program
{
    private readonly INotificationService notificationService;
    private readonly IDataRepository dataRepository;
    private readonly ISupplyService supplyService;

    public Program(INotificationService notificationService, ISupplyService supplyService, IDataRepository dataRepository)
    {
        this.notificationService = notificationService;
        this.supplyService = supplyService;
        this.dataRepository = dataRepository;
    }
    
    public async Task Run()
    {
        var currentSupply = await supplyService.FetchSupply();
        var lastSupplyAlert = dataRepository.GetLastSupplyAlert();
        var lastSupplySeen = dataRepository.GetLastSupplySeen();
        dataRepository.SetLastSupplySeen(currentSupply);
    }
    
    public static async Task Main(string[] args)
    {
        var dataFilePath = args[0];
        
        var dataRepository = new JsonRepository(dataFilePath);
        var notificationService = TelegramNotifier.Load(dataFilePath);
        var supplyService = new UltraSoundSupplyService();
        
        var program = new Program(notificationService, supplyService, dataRepository);
        await program.Run();
    }
}