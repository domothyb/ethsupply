using EthSupply.DataRepository;
using EthSupply.NotificationService;

namespace EthSupply;

public class SupplyAlerter(IDataRepository repo, INotificationService notifier, ISupplyFetcher supplyFetcher)
{
    private IDataRepository repo = repo;
    private INotificationService notifier = notifier;
    private ISupplyFetcher supplyFetcher = supplyFetcher;

    public async Task Run()
    {
        
    }
}