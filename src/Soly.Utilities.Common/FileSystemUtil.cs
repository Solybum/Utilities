using System.Reflection;

namespace Soly.Utilities.Common;
public static class FileSystemUtil
{
    public static DirectoryInfo GetAppDataDirectory(Assembly assembly)
    {
        DirectoryInfo result;
        string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string companyName = AssemblyUtil.GetCompany(assembly);
        string productName = AssemblyUtil.GetProduct(assembly);
        result = new DirectoryInfo(Path.Combine(appDataFolder, companyName, productName));
        return result;
    }
    public static FileInfo GetFileInfo(params string[] path)
    {
        FileInfo result;
        result = new FileInfo(Path.Combine(path));
        return result;
    }
}
