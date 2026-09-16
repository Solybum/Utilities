namespace Soly.Utilities.ByteArray;

public partial class ByteArray
{
    public sbyte ReadI8()
    {
        sbyte result = this.ReadI8(this.position);
        this.position += 1;
        return result;
    }
    public sbyte ReadI8(int position)
    {
        sbyte result = (sbyte)this.buffer[position];
        return result;
    }

    public void WriteI8(sbyte value)
    {
        this.WriteI8(value, this.position);
        this.position += 1;
    }
    public void WriteI8(sbyte value, int position)
    {
        this.buffer[position] = (byte)value;
    }
}
