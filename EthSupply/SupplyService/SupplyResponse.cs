using Newtonsoft.Json;

namespace EthSupply.SupplyService;

public class SupplyResponse
{
    [JsonProperty("beaconBalancesSum")] public string BeaconBalancesSum { get; set; } = null!;

    [JsonProperty("beaconDepositsSum")] public string BeaconDepositsSum { get; set; } = null!;

    [JsonProperty("ExecutionBalancesSum")] public string ExecutionBalancesSum { get; set; } = null!;
}