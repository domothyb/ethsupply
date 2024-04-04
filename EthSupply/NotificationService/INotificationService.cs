namespace EthSupply.NotificationService;

public interface INotificationService
{
    public Task Notify(string message);
}