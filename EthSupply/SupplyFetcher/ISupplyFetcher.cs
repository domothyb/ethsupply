namespace EthSupply;

public interface ISupplyFetcher
{
    public Task<double> FetchSupply();
}