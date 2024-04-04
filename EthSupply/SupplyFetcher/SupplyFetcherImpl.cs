using Newtonsoft.Json;

namespace EthSupply.SupplyFetcher
{
    public class SupplyFetcherImpl : ISupplyFetcher
    {
        public async Task<double> FetchSupply()
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://ultrasound.money/api/v2/fees/supply-parts");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<SupplyParts>(responseBody);

            var executionBalances = double.Parse(data.ExecutionBalancesSum.Substring(0, data.ExecutionBalancesSum.Length - 9)) / 1_000_000_000;
            var beaconDepositsSum = double.Parse(data.BeaconDepositsSum) / 1_000_000_000;
            var beaconBalancesSum = double.Parse(data.BeaconBalancesSum) / 1_000_000_000;
            return executionBalances + beaconBalancesSum - beaconDepositsSum;
        }
    }
}