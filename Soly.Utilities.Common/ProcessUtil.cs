using System.Diagnostics;

namespace Soly.Utilities.Common;
public static class ProcessUtil
{
    /// <summary>
    /// Start a new process using the system shell
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="arguments"></param>
    /// <param name="asAdmin"></param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="System.ComponentModel.Win32Exception"></exception>
    /// <exception cref="ObjectDisposedException"></exception>
    /// <exception cref="PlatformNotSupportedException"></exception>
    public static void Start(string fileName, string arguments, bool asAdmin)
    {
        Process p = new();
        p.StartInfo.FileName = fileName;
        p.StartInfo.Arguments = arguments;
        p.StartInfo.UseShellExecute = true;
        if (asAdmin)
        {
            p.StartInfo.Verb = "runas";
        }

        p.Start();
    }
}
