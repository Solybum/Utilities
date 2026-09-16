namespace Soly.Utilities.ByteArray;
public partial class ByteArray
{
    public void ReadArray(byte[] array, int index, int length)
    {
        this.ReadArray(array, index, length, this.position);
        this.position += (length * 1);
    }
    public void ReadArray(byte[] array, int index, int length, int position)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        length += index;
        while (index < length)
        {
            array[index] = this.ReadU8(position);
            position += 1;
            index++;
        }
    }

    public void WriteArray(byte[] array, int index, int length)
    {
        this.WriteArray(array, index, length, this.position);
        this.position += (length * 1);
    }
    public void WriteArray(byte[] array, int index, int length, int position)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        length += index;
        while (index < length)
        {
            this.WriteU8(array[index], position);
            position += 1;
            index++;
        }
    }
}
