namespace Soly.Utilities.ByteArray;

public partial class ByteArray
{
    public short ReadI16()
    {
        short result = this.ReadI16(this.position, this.Endianness);
        this.position += 2;
        return result;
    }
    public short ReadI16(int position)
    {
        short result = this.ReadI16(position, this.Endianness);
        return result;
    }
    public short ReadI16(Endianness endianness)
    {
        short result = this.ReadI16(this.position, endianness);
        this.position += 2;
        return result;
    }
    public short ReadI16(int position, Endianness endianness)
    {
        short result;
        if (endianness == Endianness.BE)
        {
            result = (short)(this.buffer[position + 1] | this.buffer[position + 0] << 8);
        }
        else
        {
            result = (short)(this.buffer[position + 0] | this.buffer[position + 1] << 8);
        }
        return result;
    }

    public void WriteI16(short value)
    {
        this.WriteI16(value, this.position, this.Endianness);
        this.position += 2;
    }
    public void WriteI16(short value, int position)
    {
        this.WriteI16(value, position, this.Endianness);
    }
    public void WriteI16(short value, Endianness endianness)
    {
        this.WriteI16(value, this.position, endianness);
        this.position += 2;
    }
    public void WriteI16(short value, int position, Endianness endianness)
    {
        if (endianness == Endianness.BE)
        {
            this.buffer[position + 1] = (byte)value;
            this.buffer[position + 0] = (byte)(value >> 8);
        }
        else
        {
            this.buffer[position + 0] = (byte)value;
            this.buffer[position + 1] = (byte)(value >> 8);
        }
    }
}
