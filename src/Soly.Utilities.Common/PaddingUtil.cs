namespace Soly.Utilities.Common;
public static class PaddingUtil
{
    public static int PadToBlock(int length, int blockSize)
    {
        return length += (blockSize - (length % blockSize)) % blockSize;
    }
}
