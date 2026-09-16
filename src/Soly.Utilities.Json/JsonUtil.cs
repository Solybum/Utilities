using System.Text.Json;

namespace Soly.Utilities.Json;

public static class JsonUtil
{
    public static string Serialize(object o, bool format = false, int maxDepth = 0)
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = format,
            IndentSize = 4,
        };

        if (maxDepth > 0)
        {
            options.MaxDepth = maxDepth;
        }

        return JsonSerializer.Serialize(o, options);
    }

    public static T? Deserialize<T>(string json)
    {
        JsonSerializerOptions options = new()
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
        };

        return JsonSerializer.Deserialize<T>(json, options);
    }
}
