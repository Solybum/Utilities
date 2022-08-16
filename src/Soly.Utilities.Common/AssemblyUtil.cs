using System.Reflection;

namespace Soly.Utilities.Common;
public static class AssemblyUtil
{
    public static string GetName(Assembly assembly)
    {
        return assembly?.GetName().Name ?? string.Empty;
    }

    public static string GetVersion(Assembly assembly)
    {
        return assembly?.GetName().Version?.ToString() ?? string.Empty;
    }

    public static string GetConfiguration(Assembly assembly)
    {
        return assembly?.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration ?? string.Empty;
    }

    public static string GetCopyright(Assembly assembly)
    {
        return assembly?.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? string.Empty;
    }

    public static string GetDescription(Assembly assembly)
    {
        return assembly?.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? string.Empty;
    }

    public static string GetCompany(Assembly assembly)
    {
        return assembly?.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? string.Empty;
    }

    public static string GetProduct(Assembly assembly)
    {
        return assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? string.Empty;
    }

    public static byte[]? ReadManifestData<TSource>(string embeddedFilename) where TSource : class
    {
        Assembly assembly = typeof(TSource).GetTypeInfo().Assembly;
        string? resourceName = assembly.GetManifestResourceNames().FirstOrDefault(s => s.EndsWith(embeddedFilename, StringComparison.CurrentCultureIgnoreCase));

        if (resourceName != null)
        {
            using Stream? stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                long length = stream.Length;
                byte[] data = new byte[length];
                using BinaryReader reader = new(stream);
                reader.Read(data, 0, data.Length);
                return data;
            }
        }
        return null;
    }
}
