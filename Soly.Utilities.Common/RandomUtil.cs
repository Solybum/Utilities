namespace Soly.Utilities.Common;
public static class RandomUtil
{
    private static readonly object Lock = new();
    private static Random random = new();

    public static void Reseed(int seed)
    {
        lock (Lock)
        {
            random = new Random(seed);
        }
    }

    public static int Next()
    {
        lock (Lock)
        {
            return random.Next();
        }
    }

    public static int Next(int maxValue)
    {
        lock (Lock)
        {
            return random.Next(maxValue);
        }
    }

    public static int Next(int minValue,int maxValue)
    {
        lock (Lock)
        {
            return random.Next(minValue, maxValue);
        }
    }

    public static void NextBytes(byte[] buffer)
    {
        lock (Lock)
        {
            random.NextBytes(buffer);
        }
    }

    public static double NextDouble()
    {
        lock (Lock)
        {
            return random.NextDouble();
        }
    }
}
