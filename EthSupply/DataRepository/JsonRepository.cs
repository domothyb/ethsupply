using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EthSupply.DataRepository;

public class JsonRepository : IDataRepository
{
    private readonly string dataFilePath;
    private readonly DataObject dataObject;
    
    public JsonRepository(string dataFilePath)
    {
        var jsonContent = File.ReadAllText(dataFilePath);
        this.dataFilePath = dataFilePath;
        this.dataObject = JsonConvert.DeserializeObject<DataObject>(jsonContent) ?? throw new InvalidOperationException();
    }
    
    public long GetLastSupplyAlert()
    {
        return dataObject.LastSupplyAlert ?? 0;
    }

    public double GetLastSupplySeen()
    {
        return dataObject.LastSupplySeen ?? 0;
    }

    public void SetLastSupplyAlert(long supply)
    {
        dataObject.LastSupplyAlert = supply;
        Save();
    }

    public void SetLastSupplySeen(double supply)
    {
        dataObject.LastSupplySeen = supply;
        Save();
    }

    private void Save()
    {
        var globalJson = JObject.Parse(File.ReadAllText(dataFilePath));
        var dataJObject = JObject.FromObject(dataObject);
        
        globalJson.Merge(dataJObject, new JsonMergeSettings
        {
            MergeArrayHandling = MergeArrayHandling.Union,
            MergeNullValueHandling = MergeNullValueHandling.Ignore
        });
        
        var jsonData = globalJson.ToString(Formatting.Indented);
        File.WriteAllText(dataFilePath, jsonData);
    }
}