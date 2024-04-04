namespace EthSupply.NotificationService;

public interface INotificationService
{
    public void AlertIncrease(long supply);

    public void AlertDecrease(long supply);
}