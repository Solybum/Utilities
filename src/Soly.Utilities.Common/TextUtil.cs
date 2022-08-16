using System.Globalization;
using System.Reflection;
using System.Text;

namespace Soly.Utilities.Common;
public static class TextUtil
{
    public static string GetAppTitle(Assembly assembly)
    {
        string name = AssemblyUtil.GetName(assembly);
        string version = AssemblyUtil.GetVersion(assembly);
        return $"{name} v{version}";
    }

    public static string RemoveWhitespace(string input)
    {
        return new string(input.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
    }

    public static string ByteCountToHumanReadable(long i)
    {
        // Get absolute value
        long absolute_i = (i < 0 ? -i : i);
        // Determine the suffix and readable value
        string suffix;
        double readable;
        if (absolute_i >= 0x1000000000000000) // Exabyte
        {
            suffix = "EB";
            readable = (i >> 50);
        }
        else if (absolute_i >= 0x4000000000000) // Petabyte
        {
            suffix = "PB";
            readable = (i >> 40);
        }
        else if (absolute_i >= 0x10000000000) // Terabyte
        {
            suffix = "TB";
            readable = (i >> 30);
        }
        else if (absolute_i >= 0x40000000) // Gigabyte
        {
            suffix = "GB";
            readable = (i >> 20);
        }
        else if (absolute_i >= 0x100000) // Megabyte
        {
            suffix = "MB";
            readable = (i >> 10);
        }
        else if (absolute_i >= 0x400) // Kilobyte
        {
            suffix = "KB";
            readable = i;
        }
        else
        {
            return i.ToString("0 B"); // Byte
        }
        // Divide by 1024 to get fractional value
        readable = (readable / 1024);
        // Return formatted number with suffix
        return readable.ToString("0.### ") + suffix;
    }

    public static byte[] HexStringToByteArray(string input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        input = RemoveWhitespace(input);

        if (input.Length % 2 != 0)
        {
            throw new ArgumentException("input must contain an even number of characters", nameof(input));
        }

        byte[] buffer = new byte[input.Length / 2];
        for (int i = 0; i < buffer.Length; i++)
        {
            string byteValue = input.Substring(i * 2, 2);
            buffer[i] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }

        return buffer;
    }

    public static string ByteArrayToHexString(byte[] bytes)
    {
        char[] c = new char[bytes.Length * 2];
        int b;
        for (int i = 0; i < bytes.Length; i++)
        {
            b = bytes[i] >> 4;
            c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
            b = bytes[i] & 0xF;
            c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
        }
        return new string(c);
    }

    public static string HexDump(byte[] bytes, int bytesPerLine = 16)
    {
        if (bytes == null)
        {
            return "<null>";
        }

        char[] HexChars = "0123456789ABCDEF".ToCharArray();
        int bytesLength = bytes.Length;
        int spacingLength = 2;
        int addressLength = 8;
        int hexDataLength = bytesPerLine * 3;
        int textDataLength = bytesPerLine;
        int lineLength = addressLength
                + spacingLength
                + hexDataLength
                + spacingLength
                + textDataLength
                + 1;

        int hexDataOffset = addressLength + spacingLength;
        int textDataOffset = hexDataOffset + hexDataLength + spacingLength;
        int newLineOffset = textDataOffset + textDataLength;

        int expectedLines = bytesLength / bytesPerLine + (bytesLength % bytesPerLine == 0 ? 0 : 1);
        StringBuilder result = new(expectedLines * lineLength);
        result.Append(' ', expectedLines * lineLength);

        int lineOffset = 0;
        for (int i = 0; i < bytesLength; i += bytesPerLine)
        {
            result[lineOffset + 0] = HexChars[(i >> 28) & 0xF];
            result[lineOffset + 1] = HexChars[(i >> 24) & 0xF];
            result[lineOffset + 2] = HexChars[(i >> 20) & 0xF];
            result[lineOffset + 3] = HexChars[(i >> 16) & 0xF];
            result[lineOffset + 4] = HexChars[(i >> 12) & 0xF];
            result[lineOffset + 5] = HexChars[(i >> 8) & 0xF];
            result[lineOffset + 6] = HexChars[(i >> 4) & 0xF];
            result[lineOffset + 7] = HexChars[(i >> 0) & 0xF];
            result[lineOffset + newLineOffset] = '\n';

            for (int j = 0; j < bytesPerLine; j++)
            {
                if (i + j < bytesLength)
                {
                    byte b = bytes[i + j];
                    result[lineOffset + hexDataOffset + j * 3] = HexChars[(b >> 4) & 0xF];
                    result[lineOffset + hexDataOffset + j * 3 + 1] = HexChars[b & 0xF];
                    result[lineOffset + textDataOffset + j] = (b < 32 ? '·' : (char)b);
                }
                else
                {
                    result[lineOffset + hexDataOffset + j * 3] = ' ';
                    result[lineOffset + hexDataOffset + j * 3 + 1] = ' ';
                    result[lineOffset + textDataOffset + j] = ' ';
                }
            }
            lineOffset += lineLength;
        }

        return result.ToString();
    }
}