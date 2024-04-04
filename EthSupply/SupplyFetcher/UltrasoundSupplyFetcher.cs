using Newtonsoft.Json;

namespace EthSupply.SupplyFetcher
{
    public class UltrasoundSupplyFetcher : ISupplyFetcher
    {
        private const string ULTRA_SOUND_SUPPLY_API_URL = "https://ultrasound.money/api/v2/fees/supply-parts";

        public async Task<double> FetchSupply()
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync(ULTRA_SOUND_SUPPLY_API_URL);
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