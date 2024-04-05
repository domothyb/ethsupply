using EthSupply.DataRepository;
using EthSupply.NotificationService;
using EthSupply.SupplyService;

namespace EthSupply;
using NSubstitute;

public class Tests
{
    private INotificationService notificationService = null!;
    private ISupplyService supplyService = null!;
    private IDataRepository dataRepository = null!;

    private Program program = null!;
    
    [SetUp]
    public void Setup()
    {
        notificationService = Substitute.For<INotificationService>();
        dataRepository = Substitute.For<IDataRepository>();
        supplyService = Substitute.For<ISupplyService>();
        dataRepository.GetLastSupplyAlert().Returns(0);
        
        program = new Program(notificationService, supplyService, dataRepository);
    }

    [Test]
    public async Task Run_CallsFetchSupply()
    {
        await program.Run();
        await supplyService.Received(1).FetchSupply();
    }

    [Test]
    public async Task Run_CallsGetLastSupplySeen()
    {
        await program.Run();
        dataRepository.Received(1).GetLastSupplySeen();
    }

    [Test]
    public async Task Run_UpdatesLastSupplySeenWithFetchedSupply()
    {
        const double fetchedSupply = 2000.0;
        supplyService.FetchSupply().Returns(Task.FromResult(fetchedSupply));

        await program.Run();

        dataRepository.Received(1).SetLastSupplySeen(fetchedSupply);
    }

    [Test]
    public async Task Scenario1_WentDown_NotPastThreshold_ShouldNotAlert()
    {
        LastSupplySeenWas(100_400_305);
        CurrentSupplyIs(100_400_300);

        await program.Run();
        
        DidntAlert();
    }
    
    [Test]
    public async Task Scenario2_WentDown_PastNewThreshold_ShoudAlertDown()
    {
        LastSupplySeenWas(100_400_005);
        CurrentSupplyIs(100_399_995);
        LastSupplyAlertedWas(100_500_000);

        await program.Run();
        
        AlertedDecrease(100_400_000);
    }
    
    [Test]
    public async Task Scenario3_WentUp_NotPastThreshold_ShouldNotAlert()
    {
        LastSupplySeenWas(100_400_300);
        CurrentSupplyIs(100_400_305);

        await program.Run();
        
        DidntAlert();
    }
    
    [Test]
    public async Task Scenario4_WentUp_PastNewThreshold_ShoudAlertUp()
    {
        LastSupplySeenWas(100_399_995);
        CurrentSupplyIs(100_400_005);
        LastSupplyAlertedWas(100_300_000);

        await program.Run();
        
        AlertedIncrease(100_400_000);
    }
    
    [Test]
    public async Task Scenario5_WentUp_SeenThreshold_ShoudNotAlert()
    {
        LastSupplySeenWas(100_399_995);
        CurrentSupplyIs(100_400_005);
        LastSupplyAlertedWas(100_400_000);

        await program.Run();
        
        DidntAlert();
    }
    
    [Test]
    public async Task Scenario6_WentDown_SeenThreshold_ShoudNotAlert()
    {
        LastSupplySeenWas(100_400_005);
        CurrentSupplyIs(100_399_005);
        LastSupplyAlertedWas(100_400_000);

        await program.Run();
        
        DidntAlert();
    }

    private void AlertedDecrease(long supply)
    {
        notificationService.Received().AlertDecrease(supply);
    }

    private void AlertedIncrease(long supply)
    {
        notificationService.Received().AlertIncrease(supply);
    }
    
    private void LastSupplySeenWas(double supply)
    {
        dataRepository.GetLastSupplySeen().Returns(supply);
    }
    
    private void LastSupplyAlertedWas(long supply)
    {
        dataRepository.GetLastSupplyAlert().Returns(supply);
    }

    private void DidntAlert()
    {
        notificationService.DidNotReceive().AlertIncrease(Arg.Any<long>());
        notificationService.DidNotReceive().AlertDecrease(Arg.Any<long>());
    }

    private void CurrentSupplyIs(double supply)
    {
        supplyService.FetchSupply().Returns(Task.FromResult(supply));
    }
}