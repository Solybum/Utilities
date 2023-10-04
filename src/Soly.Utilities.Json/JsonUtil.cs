using Newtonsoft.Json;

namespace Soly.Utilities.Json;
public static class JsonUtil
{
    public static string Serialize(object o, bool format = false, int maxDepth = 0)
    {
        JsonSerializerSettings settings = new()
        {
            Formatting = format ? Formatting.Indented : Formatting.None,
        };
        return JsonConvert.SerializeObject(o, settings);
    }
    public static T? Deserialize<T>(string json)
    {
        JsonSerializerSettings settings = new()
        {
            ObjectCreationHandling = ObjectCreationHandling.Replace,
        };
        return JsonConvert.DeserializeObject<T>(json, settings);
    }
}
