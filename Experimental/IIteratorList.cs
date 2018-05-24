using System;
using System.Collections.Generic;

namespace QuantTC.Experimental
{
    public interface IIteratorList: IReadOnlyList<object>
    {
        [Obsolete]
        int Size { get; }
        [Obsolete]
        object GetValue(int index);
    }
}