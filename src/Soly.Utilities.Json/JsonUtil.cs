using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Soly.Utilities.Json;

public static class JsonUtil
{
    public static string Serialize<T>(T data, JsonTypeInfo<T> typeInfo)
    {
        return JsonSerializer.Serialize(data, typeInfo);
    }

    public static T? Deserialize<T>(string json, JsonTypeInfo<T> typeInfo)
    {
        return JsonSerializer.Deserialize(json, typeInfo);
    }
}
