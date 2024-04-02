namespace EthSupply;

public interface ISupplyFetcher
{
    public Task<long> FetchSupply();
}