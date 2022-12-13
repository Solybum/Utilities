using CommunityToolkit.Diagnostics;

namespace Soly.Utilities.ByteArray;
public partial class ByteArray
{
    private readonly byte[] temp = new byte[8];
    private byte[] buffer;
    private int position;

    public byte[] Buffer
    {
        get { return this.buffer; }
    }

    public int Length
    {
        get
        {
            return this.buffer.Length;
        }
    }
    public int Position
    {
        get
        {
            return this.position;
        }
        set
        {
            Guard.IsInRange(value, 0, this.buffer.Length + 1);
            this.position = value;
        }
    }

    public Endianness Endianness { get; set; }

    public ByteArray(int size) : this(size, Endianness.LE)
    {
    }

    public ByteArray(int size, Endianness endianness)
    {
        if (size < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size));
        }
        this.buffer = new byte[size];
        this.Endianness = endianness;
    }

    public ByteArray(byte[] byteArray) : this(byteArray, Endianness.LE)
    {
    }
    public ByteArray(byte[] byteArray, Endianness endianness)
    {
        Guard.IsNotNull(byteArray);
        
        this.buffer = byteArray;
        this.Endianness = endianness;
    }
    public ByteArray(ByteArray byteArray)
    {
        Guard.IsNotNull(byteArray);

        this.buffer = new byte[byteArray.Length];
        this.Endianness = byteArray.Endianness;

        byteArray.position = 0;
        Array.Copy(byteArray.Buffer, this.Buffer, byteArray.Length);
    }

    public void Resize(int size)
    {
        if (size == this.buffer.Length)
        {
            return;
        }
        Guard.IsGreaterThanOrEqualTo(size, 0);
        Guard.IsLessThanOrEqualTo(size, int.MaxValue);

        Array.Resize(ref this.buffer, size);
        if (this.position > size)
        {
            this.position = size;
        }
    }
    public void Clear()
    {
        this.Clear(this.buffer.Length, 0);
        this.position = 0;
    }
    public void Clear(int length)
    {
        Array.Clear(this.buffer, this.position, length);
        this.position += length;
    }
    public void Clear(int length, int position)
    {
        Array.Clear(this.buffer, position, length);
    }

    public void Fill(byte value)
    {
        this.Fill(value, 0, this.buffer.Length);
    }
    public void Fill(byte value, int index, int length)
    {
        for (int i1 = index; i1 < length; i1++)
        {
            this.buffer[i1] = value;
        }
    }
    public override string ToString()
    {
        int tslen = this.Length - this.position;
        if (tslen > 16)
        {
            tslen = 16;
        }
        return $"0x{this.position:X8}: {BitConverter.ToString(this.buffer, this.position, tslen).Replace("-", " ")}";
    }
    public byte this[int offset]
    {
        get { return this.buffer[offset]; }
        set { this.buffer[offset] = value; }
    }
    public void Pad(int padding)
    {
        while ((this.position % padding) != 0 && this.position < this.buffer.Length)
        {
            this.buffer[this.position++] = 0;
        }
    }
}
