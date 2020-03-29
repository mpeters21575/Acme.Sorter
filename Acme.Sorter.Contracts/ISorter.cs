using System.Collections.Generic;

namespace Acme.Sorter.Contracts
{
    public interface ISorter
    {
        IEnumerable<string> Sort();
        string[] Text { get; set; }
    }
}
