using System.Collections.Generic;

namespace QuantTC.Indicators.Generic
{
    public interface ITreeView: INamedConcept
    {
        IEnumerable<ITreeView> GetNexts();
    }
}