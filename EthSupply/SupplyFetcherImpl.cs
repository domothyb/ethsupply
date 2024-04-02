using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EthSupply
{
    public class SupplyFetcherImpl : ISupplyFetcher
    {
        public async Task<long> FetchSupply()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://ultrasound.money/api/v2/fees/supply-parts");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SupplyParts>(responseBody);
                
                //     return int(req["executionBalancesSum"]) * 10**-18 + int(req["beaconBalancesSum"]) * 10**-9 - int(req["beaconDepositsSum"]) * 10**-9
                
                return (double.Parse(data.ExecutionBalancesSum) * Math.Pow(10,-18));
            }
        }
    }
}