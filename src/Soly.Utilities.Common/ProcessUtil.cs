using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Soly.Utilities.Common;

public static class ProcessUtil
{
    /// <summary>
    /// Start a new process
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="arguments"></param>
    /// <param name="elevated"></param>
    /// <param name="useShellExecute"></param>
    public static Process Start(
        string fileName,
        IEnumerable<string>? arguments = null,
        bool elevated = false,
        bool useShellExecute = false)
    {
        if (elevated && useShellExecute)
        {
            throw new ArgumentException("Process cannot be launched with both 'elevated' and 'useShellExecute' set to true.");
        }

        Process process = new();
        arguments ??= [];

        process.StartInfo.CreateNoWindow = true;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            process.StartInfo.FileName = fileName;

            if (elevated)
            {
                process.StartInfo.UseShellExecute = true; // Required for Windows UAC 'runas'
                process.StartInfo.Verb = "runas";
                process.StartInfo.Arguments = string.Join(" ", arguments);
            }
            else
            {
                process.StartInfo.UseShellExecute = useShellExecute;

                if (useShellExecute)
                {
                    process.StartInfo.Arguments = string.Join(" ", arguments);
                }
                else
                {
                    foreach (var arg in arguments)
                    {
                        process.StartInfo.ArgumentList.Add(arg);
                    }
                }
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            if (elevated)
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.FileName = "osascript";
                string flatArgs = string.Join(" ", arguments);
                process.StartInfo.Arguments = $"-e \"do shell script \\\"{fileName} {flatArgs}\\\" with administrator privileges\"";
            }
            else
            {
                process.StartInfo.FileName = fileName;
                process.StartInfo.UseShellExecute = useShellExecute;

                if (useShellExecute)
                {
                    process.StartInfo.Arguments = string.Join(" ", arguments);
                }
                else
                {
                    foreach (var arg in arguments)
                    {
                        process.StartInfo.ArgumentList.Add(arg);
                    }
                }
            }
        }
        else
        {
            if (elevated)
            {
                process.StartInfo.UseShellExecute = false;
                // Terminal context uses sudo, GUI context uses pkexec
                process.StartInfo.FileName = !Console.IsOutputRedirected ? "sudo" : "pkexec";

                process.StartInfo.ArgumentList.Add(fileName);
                foreach (var arg in arguments)
                {
                    process.StartInfo.ArgumentList.Add(arg);
                }
            }
            else
            {
                process.StartInfo.FileName = fileName;
                process.StartInfo.UseShellExecute = useShellExecute;

                if (useShellExecute)
                {
                    process.StartInfo.Arguments = string.Join(" ", arguments);
                }
                else
                {
                    foreach (var arg in arguments)
                    {
                        process.StartInfo.ArgumentList.Add(arg);
                    }
                }
            }
        }

        process.Start();
        return process;
    }
}
