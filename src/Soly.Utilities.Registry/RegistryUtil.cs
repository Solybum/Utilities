using Microsoft.Win32;

namespace Soly.Utilities.Registry;
public class RegistryUtil
{
    public static bool SubKeyExist(RegistryKey key, string subkey)
    {
        return key.OpenSubKey(subkey) != null;
    }
    public static void DeleteSubKey(RegistryKey key, string subkey)
    {
        key.DeleteSubKey(subkey);
    }
    public static void RenameSubKey(RegistryKey key, string subkey, string name)
    {
        CopySubKey(key, subkey, name);
        key.DeleteSubKeyTree(subkey);
    }
    public static void CopySubKey(RegistryKey key, string subkey, string newSubKey)
    {
        RegistryKey destinationKey = key.CreateSubKey(newSubKey);
        RegistryKey? sourceKey = key.OpenSubKey(subkey);
        if (sourceKey != null)
        {
            RecurseCopySubKey(sourceKey, destinationKey);
        }
    }
    private static void RecurseCopySubKey(RegistryKey sourceKey, RegistryKey destinationKey)
    {
        foreach (string name in sourceKey.GetValueNames())
        {
            object? value = sourceKey.GetValue(name);
            if (value != null)
            {
                RegistryValueKind kind = sourceKey.GetValueKind(name);
                destinationKey.SetValue(name, value, kind);
            }
        }

        foreach (string sourceSubKeyName in sourceKey.GetSubKeyNames())
        {
            RegistryKey? sourceSubKey = sourceKey.OpenSubKey(sourceSubKeyName);
            RegistryKey destSubKey = destinationKey.CreateSubKey(sourceSubKeyName);
            if (sourceSubKey != null)
            {
                RecurseCopySubKey(sourceSubKey, destSubKey);
            }
        }
    }

    public static void RenameValue(RegistryKey key, string subkey, string oldName, string newName)
    {
        key = key.CreateSubKey(subkey);
        object? value = key.GetValue(oldName);
        RegistryValueKind kind = key.GetValueKind(oldName);
        if (value != null)
        {
            key.SetValue(newName, value, kind);
            key.DeleteValue(oldName);
        }
    }
    public static void DeleteValue(RegistryKey key, string subkey, string name)
    {
        key = key.CreateSubKey(subkey);
        if (key.GetValue(name) != null)
        {
            key.DeleteValue(name);
        }
    }

    public static int GetDWord(RegistryKey key, string subkey, string name, int def)
    {
        key = key.CreateSubKey(subkey);
        if (key.GetValue(name) != null)
        {
            object? value = key.GetValue(name);
            if (value != null)
            {
                return (int)value;
            }
        }
        return def;
    }
    public static void SetDWord(RegistryKey key, string subkey, string name, int value)
    {
        key = key.CreateSubKey(subkey);
        key.SetValue(name, value, RegistryValueKind.DWord);
    }
    public static byte[] GetBinary(RegistryKey key, string subkey, string name)
    {
        key = key.CreateSubKey(subkey);
        if (key.GetValue(name) != null)
        {
            object? value = key.GetValue(name);
            if (value != null)
            {
                return (byte[])value;
            }
        }
        return Array.Empty<byte>();
    }
    public static void SetBinary(RegistryKey key, string subkey, string name, byte[] value)
    {
        key = key.CreateSubKey(subkey);
        key.SetValue(name, value, RegistryValueKind.Binary);
    }
    public static string GetString(RegistryKey key, string subkey, string name, string def)
    {
        key = key.CreateSubKey(subkey);
        if (key.GetValue(name) != null)
        {
            object? value = key.GetValue(name);
            if (value != null)
            {
                return (string)value;
            }
        }
        return def;
    }
    public static void SetString(RegistryKey key, string subkey, string name, string value)
    {
        key = key.CreateSubKey(subkey);
        key.SetValue(name, value, RegistryValueKind.String);
    }
    public static string GetExpandString(RegistryKey key, string subkey, string name, string def)
    {
        key = key.CreateSubKey(subkey);
        if (key.GetValue(name) != null)
        {
            object? value = key.GetValue(name);
            if (value != null)
            {
                return (string)value;
            }
        }
        return def;
    }
    public static void SetExpandString(RegistryKey key, string subkey, string name, string value)
    {
        key = key.CreateSubKey(subkey);
        key.SetValue(name, value, RegistryValueKind.ExpandString);
    }
    public static string[] GetMultiString(RegistryKey key, string subkey, string name)
    {
        key = key.CreateSubKey(subkey);
        if (key.GetValue(name) != null)
        {
            object? value = key.GetValue(name);
            if (value != null)
            {
                return (string[])value;
            }
        }
        return Array.Empty<string>();
    }
    public static void SetMultiString(RegistryKey key, string subkey, string name, string[] value)
    {
        key = key.CreateSubKey(subkey);
        key.SetValue(name, value, RegistryValueKind.MultiString);
    }
}
