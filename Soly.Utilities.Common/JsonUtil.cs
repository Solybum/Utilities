using Newtonsoft.Json;
using System.Text;

namespace Soly.Utilities.Common;
public static class JsonUtil
{
    public static string Serialize(object data, bool format = false)
    {
        StringBuilder sb = new();
        JsonTextWriter jw = new(new StringWriter(sb));
        JsonSerializer js = new();

        if (format)
        {
            jw.Formatting = Formatting.Indented;
            jw.Indentation = 1;
            jw.IndentChar = '\t';
        }

        js.Serialize(jw, data);

        return sb.ToString();
    }
    public static T? Deserialize<T>(byte[] data, int offset, int count)
    {
        using StreamReader sr = new(new MemoryStream(data, offset, count));
        JsonSerializer json = new()
        {
            ObjectCreationHandling = ObjectCreationHandling.Replace
        };
        return json.Deserialize<T>(new JsonTextReader(sr));
    }
}
