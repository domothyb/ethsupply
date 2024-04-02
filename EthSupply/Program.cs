namespace EthSupply;

class Program
{
    public static void Main(string[] args)
    {
        var supplyFetcher = new SupplyFetcherImpl();
        
        Console.WriteLine(supplyFetcher.FetchSupply().Result);
    }
}