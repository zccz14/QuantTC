namespace QuantTC.Experimental
{
    public interface IIteratorList
    {
        long Size { get; }
        object GetValue(int index);
    }
}