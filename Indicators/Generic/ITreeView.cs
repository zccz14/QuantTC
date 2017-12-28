using System.Collections.Generic;

namespace QuantTC.Indicators.Generic
{
    public interface ITreeView: ITitle
    {
        IEnumerable<ITreeView> GetNexts();
    }
}