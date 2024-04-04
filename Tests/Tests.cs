using EthSupply.DataRepository;
using EthSupply.NotificationService;

namespace EthSupply;
using NSubstitute;

public class Tests
{
    private INotificationService notificationService;
    private ISupplyFetcher supplyFetcher;
    private IDataRepository dataRepository;

    private Program program;
    
    [SetUp]
    public void Setup()
    {
        notificationService = Substitute.For<INotificationService>();
        supplyFetcher = Substitute.For<ISupplyFetcher>();
        dataRepository = Substitute.For<IDataRepository>();

        program = new Program(notificationService, supplyFetcher, dataRepository);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}