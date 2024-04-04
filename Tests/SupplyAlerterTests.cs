using EthSupply.DataRepository;
using EthSupply.NotificationService;

namespace EthSupply;
using Moq;
using NUnit.Framework;

public class Tests
{
    private Mock<ISupplyFetcher> supplyFetcher;
    private Mock<INotificationService> notificationService;
    private Mock<IDataRepository> dataRepository;

    private SupplyAlerter bot;
    
    [SetUp]
    public void Setup()
    {
        supplyFetcher = new Mock<ISupplyFetcher>();
        notificationService = new Mock<INotificationService>();
        dataRepository = new Mock<IDataRepository>();

        bot = new SupplyAlerter(dataRepository, notificationService, supplyFetcher);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}