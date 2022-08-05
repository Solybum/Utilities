using System.IO.Compression;

namespace Soly.Utilities.Common;
public static class DeflateUtil
{
    public static class DeflateHelper
    {
        public static byte[] Compress(byte[] data, int offset, int length)
        {
            using MemoryStream input = new(data, offset, length);
            using MemoryStream output = new();
            using (DeflateStream ds = new(output, CompressionMode.Compress))
            {
                input.CopyTo(ds);
            }
            return output.ToArray();
        }
        public static byte[] Decompress(byte[] data, int offset, int length)
        {
            using MemoryStream input = new(data, offset, length);
            using MemoryStream output = new();
            using (DeflateStream ds = new(input, CompressionMode.Decompress))
            {
                ds.CopyTo(output);
            }
            return output.ToArray();
        }
    }
}
