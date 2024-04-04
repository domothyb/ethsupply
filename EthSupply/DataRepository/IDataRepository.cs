namespace EthSupply.DataRepository;

public interface IDataRepository
{
    string GetBotToken();
    long GetChannelId();
    double GetLastSupplyAlert();
    double GetLastSupplySeen();
    void SetLastSupplyAlert(double supply);
    void SetLastSupplySeen(double supply);
}