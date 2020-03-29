using Acme.Sorter.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Sorter.Domain.Strategies
{
    [SorterType(SorterType = Contracts.Models.SorterType.TextLength)]
    public class LengthSorter : ISorter
    {
        public string[] Text { get; set; }
        
        public IEnumerable<string> Sort()
        {
            return Text.OrderBy(q => q.Length);
        }
    }
}
