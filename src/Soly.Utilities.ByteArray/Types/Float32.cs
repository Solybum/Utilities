namespace Soly.Utilities.ByteArray;

public partial class ByteArray
{
    public float ReadF32()
    {
        float result = this.ReadF32(this.position, this.Endianness);
        this.position += 4;
        return result;
    }
    public float ReadF32(int position)
    {
        float result = this.ReadF32(position, this.Endianness);
        return result;
    }
    public float ReadF32(Endianness endianness)
    {
        float result = this.ReadF32(this.position, endianness);
        this.position += 4;
        return result;
    }
    public float ReadF32(int position, Endianness endianness)
    {
        float result;
        if (endianness == Endianness.BE)
        {
            lock (this.temp)
            {
                this.temp[0] = this.buffer[position + 3];
                this.temp[1] = this.buffer[position + 2];
                this.temp[2] = this.buffer[position + 1];
                this.temp[3] = this.buffer[position + 0];
                result = BitConverter.ToSingle(this.temp, 0);
            }
        }
        else
        {
            result = BitConverter.ToSingle(this.buffer, position);
        }
        return result;
    }

    public void WriteF32(float value)
    {
        this.WriteF32(value, this.position, this.Endianness);
        this.position += 4;
    }
    public void WriteF32(float value, int position)
    {
        this.WriteF32(value, position, this.Endianness);
    }
    public void WriteF32(float value, Endianness endianness)
    {
        this.WriteF32(value, this.position, endianness);
        this.position += 4;
    }
    public void WriteF32(float value, int position, Endianness endianness)
    {
        lock (this.temp)
        {
            Array.Copy(BitConverter.GetBytes(value), 0, this.temp, 0, 4);
            if (endianness == Endianness.BE)
            {
                this.buffer[position + 3] = this.temp[0];
                this.buffer[position + 2] = this.temp[1];
                this.buffer[position + 1] = this.temp[2];
                this.buffer[position + 0] = this.temp[3];
            }
            else
            {
                this.buffer[position + 0] = this.temp[0];
                this.buffer[position + 1] = this.temp[1];
                this.buffer[position + 2] = this.temp[2];
                this.buffer[position + 3] = this.temp[3];
            }
        }
    }
}
