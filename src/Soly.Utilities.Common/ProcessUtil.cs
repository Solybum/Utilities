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
    public static Process Start(string fileName, string arguments = "", bool asAdmin = false)
    {
        Process process = new();
        process.StartInfo.FileName = fileName;
        process.StartInfo.Arguments = arguments;
        process.StartInfo.UseShellExecute = true;
        if (asAdmin)
        {
            process.StartInfo.Verb = "runas";
        }

        process.Start();
        return process;
    }
}
