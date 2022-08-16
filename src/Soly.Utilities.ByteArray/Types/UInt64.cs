﻿namespace Soly.Utilities.ByteArray;

public partial class ByteArray
{
    public ulong ReadU64()
    {
        ulong result = this.ReadU64(this.position, this.Endianness);
        this.position += 8;
        return result;
    }
    public ulong ReadU64(int position)
    {
        ulong result = this.ReadU64(position, this.Endianness);
        return result;
    }
    public ulong ReadU64(Endianness endianness)
    {
        ulong result = this.ReadU64(this.position, endianness);
        this.position += 8;
        return result;
    }
    public ulong ReadU64(int position, Endianness endianness)
    {
        ulong result;
        if (endianness == Endianness.BE)
        {
            result = this.buffer[position + 7];
            result |= (ulong)this.buffer[position + 6] << 8;
            result |= (ulong)this.buffer[position + 5] << 16;
            result |= (ulong)this.buffer[position + 4] << 24;
            result |= (ulong)this.buffer[position + 3] << 32;
            result |= (ulong)this.buffer[position + 2] << 40;
            result |= (ulong)this.buffer[position + 1] << 48;
            result |= (ulong)this.buffer[position + 0] << 56;
        }
        else
        {
            result = this.buffer[position + 0];
            result |= (ulong)this.buffer[position + 1] << 8;
            result |= (ulong)this.buffer[position + 2] << 16;
            result |= (ulong)this.buffer[position + 3] << 24;
            result |= (ulong)this.buffer[position + 4] << 32;
            result |= (ulong)this.buffer[position + 5] << 40;
            result |= (ulong)this.buffer[position + 6] << 48;
            result |= (ulong)this.buffer[position + 7] << 56;
        }
        return result;
    }

    public void Write(ulong value)
    {
        this.Write(value, this.position, this.Endianness);
        this.position += 8;
    }
    public void Write(ulong value, int position)
    {
        this.Write(value, position, this.Endianness);
    }
    public void Write(ulong value, Endianness endianness)
    {
        this.Write(value, this.position, endianness);
        this.position += 8;
    }
    public void Write(ulong value, int position, Endianness endianness)
    {
        if (endianness == Endianness.BE)
        {
            this.buffer[position + 7] = (byte)value;
            this.buffer[position + 6] = (byte)(value >> 8);
            this.buffer[position + 5] = (byte)(value >> 16);
            this.buffer[position + 4] = (byte)(value >> 24);
            this.buffer[position + 3] = (byte)(value >> 32);
            this.buffer[position + 2] = (byte)(value >> 40);
            this.buffer[position + 1] = (byte)(value >> 48);
            this.buffer[position + 0] = (byte)(value >> 56);
        }
        else
        {
            this.buffer[position + 0] = (byte)value;
            this.buffer[position + 1] = (byte)(value >> 8);
            this.buffer[position + 2] = (byte)(value >> 16);
            this.buffer[position + 3] = (byte)(value >> 24);
            this.buffer[position + 4] = (byte)(value >> 32);
            this.buffer[position + 5] = (byte)(value >> 40);
            this.buffer[position + 6] = (byte)(value >> 48);
            this.buffer[position + 7] = (byte)(value >> 56);
        }
    }
}
