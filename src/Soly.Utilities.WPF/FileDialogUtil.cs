using Microsoft.Win32;

namespace Soly.Utilities.WPF;
public static class FileDialogUtil
{
    public static string LoadFile(
        string filter = "All files (*.*)|*.*",
        string initialDirectory = "")
    {
        OpenFileDialog dialog = new()
        {
            Filter = filter,
            InitialDirectory = initialDirectory,
        };
        bool? result = dialog.ShowDialog();
        if (result == true)
        {
            return dialog.FileName;
        }
        return string.Empty;
    }
    public static string SaveFile(
        string filter = "All files (*.*)|*.*",
        string initialDirectory = "",
        string fileName = "",
        bool addExtension = true,
        bool overwritePrompt = true)
    {
        SaveFileDialog dialog = new()
        {
            Filter = filter,
            InitialDirectory = initialDirectory,
            FileName = fileName,
            OverwritePrompt = overwritePrompt,
            AddExtension = addExtension,
        };

        bool? result = dialog.ShowDialog();
        if (result == true)
        {
            return dialog.FileName;
        }

        return string.Empty;
    }
}