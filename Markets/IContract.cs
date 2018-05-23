using System;
using System.Collections.Generic;
using System.Text;

namespace QuantTC.Markets
{
    public interface IProduct: INamedConcept
    {
        ProductType Type { get; }
    }
    public interface IContract
    {
        
    }
}
