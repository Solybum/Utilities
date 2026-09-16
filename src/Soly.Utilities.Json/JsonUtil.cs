using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Soly.Utilities.Json;

public static class JsonUtil
{
    public static string Serialize<T>(T value, JsonSerializerContext context, bool format = false) where T : class
    {
        if (context.GetTypeInfo(typeof(T)) is not JsonTypeInfo<T> typeInfo)
        {
            throw new InvalidOperationException(
                $"Type {typeof(T).Name} is not registered in the provided JsonSerializerContext."
            );
        }

        if (format)
        {
            var indentedOptions = new JsonSerializerOptions(typeInfo.Options)
            {
                WriteIndented = true,
                IndentSize = 4
            };

            JsonTypeInfo indentedTypeInfo = indentedOptions.GetTypeInfo(typeof(T));

            return JsonSerializer.Serialize(value, indentedTypeInfo);
        }

        return JsonSerializer.Serialize(value, typeInfo);
    }

    public static T? Deserialize<T>(string json, JsonSerializerContext context) where T : class
    {
        if (context.GetTypeInfo(typeof(T)) is not JsonTypeInfo<T> typeInfo)
        {
            throw new InvalidOperationException(
                $"Type {typeof(T).Name} is not registered in the provided JsonSerializerContext."
            );
        }

        context.Options.ReadCommentHandling = JsonCommentHandling.Skip;
        context.Options.AllowTrailingCommas = true;

        return JsonSerializer.Deserialize(json, typeInfo);
    }
}
