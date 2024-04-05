using EthSupply.DataRepository;
using EthSupply.NotificationService;
using EthSupply.SupplyService;

namespace EthSupply;

public class Program
{
    private readonly IDataRepository dataRepository;

    private readonly INotificationService notificationService;
    private readonly ISupplyService supplyService;

    private double currentSupply;

    public Program(INotificationService notificationService, ISupplyService supplyService,
        IDataRepository dataRepository)
    {
        this.notificationService = notificationService;
        this.supplyService = supplyService;
        this.dataRepository = dataRepository;
    }

    private static long RoundTo1000(double value)
    {
        return (long)Math.Round(value / 1000) * 1000;
    }

    public async Task Run()
    {
        currentSupply = await supplyService.FetchSupply();

        if (SupplyWentPastThreshold())
            await NotifySupplyChange();
    }

    private async Task NotifySupplyChange()
    {
        var lastSupplyAlert = dataRepository.GetLastSupplyAlert();
        var newSupplyAlert = RoundTo1000(currentSupply);

        if (lastSupplyAlert == 0)
        {
            dataRepository.SetLastSupplyAlert(newSupplyAlert);
            return;
        }

        if (newSupplyAlert == lastSupplyAlert)
            return;

        if (newSupplyAlert < lastSupplyAlert)
            await notificationService.AlertDecrease(newSupplyAlert);
        if (newSupplyAlert > lastSupplyAlert)
            await notificationService.AlertIncrease(newSupplyAlert);

        dataRepository.SetLastSupplyAlert(newSupplyAlert);
    }

    private bool SupplyWentPastThreshold()
    {
        var threshold = RoundTo1000(currentSupply);
        var lastSupplySeen = dataRepository.GetLastSupplySeen();
        dataRepository.SetLastSupplySeen(currentSupply);

        var min = Math.Min(currentSupply, lastSupplySeen);
        var max = Math.Max(currentSupply, lastSupplySeen);

        return min < threshold && threshold < max;
    }

    public static async Task Main(string[] args)
    {
        var dataFilePath = args[0];

        var dataRepository = new JsonRepository(dataFilePath);
        var notificationService = TelegramNotifier.Load(dataFilePath);
        var supplyService = new UltraSoundAPISupplyService();

        var program = new Program(notificationService, supplyService, dataRepository);
        await program.Run();
    }
}