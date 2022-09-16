using System.Text.Json;
using System.Text.Json.Serialization;

namespace Soly.Utilities.Common;
public static class JsonUtil
{
    public static string Serialize(object o, bool format = false, int maxDepth = 0)
    {
        JsonSerializerOptions options = new()
        {
            AllowTrailingCommas = true,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals | JsonNumberHandling.AllowReadingFromString,
            ReadCommentHandling = JsonCommentHandling.Skip,
            ReferenceHandler = ReferenceHandler.Preserve,

            MaxDepth = maxDepth,
            WriteIndented = format,
        };
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        return JsonSerializer.Serialize(o, options);
    }
    public static T? Deserialize<T>(string json)
    {
        JsonSerializerOptions options = new()
        {
            AllowTrailingCommas = true,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals | JsonNumberHandling.AllowReadingFromString,
            ReadCommentHandling = JsonCommentHandling.Skip,
            ReferenceHandler = ReferenceHandler.Preserve,
        };
        options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        return JsonSerializer.Deserialize<T>(json, options);
    }
}
