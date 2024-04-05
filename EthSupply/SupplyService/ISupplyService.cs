namespace EthSupply.SupplyService;

public interface ISupplyService
{
    public Task<double> FetchSupply();
}