namespace EthSupply.NotificationService;

public interface INotificationService
{
    public Task AlertIncrease(long supply);

    public Task AlertDecrease(long supply);
}