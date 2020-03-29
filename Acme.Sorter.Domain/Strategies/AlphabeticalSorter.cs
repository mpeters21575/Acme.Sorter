using Acme.Sorter.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Sorter.Domain.Strategies
{
    public class AlphabeticalSorter : ISorter
    {
        public string[] Text { get; set; }

        public IEnumerable<string> Sort()
        {
            return Text.OrderBy(q => q);
        }
    }
}
