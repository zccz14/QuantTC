using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public interface IIteratorList: IReadOnlyList<object>
    {
        int Size { get; }
        object GetValue(int index);
    }
}