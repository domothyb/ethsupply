using System.Xml.Linq;
using Newtonsoft.Json;

namespace EthSupply.DataRepository;

public class JsonRepository : IDataRepository
{
    private string dataFilePath;
    private DataObject dataObject;
    
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
        string jsonData = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
        File.WriteAllText(dataFilePath, jsonData);
    }
}