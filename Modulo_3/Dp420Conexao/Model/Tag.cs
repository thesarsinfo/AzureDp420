using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
public class Tag
{
    [JsonProperty("id")]
    public  Guid Id {get;set;}        
    public  string? Name {get;set;}
    public  bool Online {get;set;}    
    public Tag(Guid id, string? name, bool online)
    {
        this.Id = id;
        this.Name = name;
        this.Online = online;
    }

    public Tag()
    {
    }
}