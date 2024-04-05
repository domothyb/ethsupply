namespace EthSupply.DataRepository;

public interface IDataRepository
{
    long GetLastSupplyAlert();
    double GetLastSupplySeen();
    void SetLastSupplyAlert(long supply);
    void SetLastSupplySeen(double supply);
}