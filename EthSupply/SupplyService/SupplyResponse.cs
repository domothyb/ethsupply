using Newtonsoft.Json;

namespace EthSupply.SupplyService;

public class SupplyResponse
{
    [JsonProperty("beaconBalancesSum")]
    public string BeaconBalancesSum { get; set; }

    [JsonProperty("beaconDepositsSum")]
    public string BeaconDepositsSum { get; set; }

    [JsonProperty("blockNumber")]
    public int BlockNumber { get; set; }

    [JsonProperty("ExecutionBalancesSum")]
    public string ExecutionBalancesSum { get; set;  }
}