using Acme.Sorter.Contracts;
using System.Collections.Generic;

namespace Acme.Sorter.Domain.Strategies
{
    public class BubbleSorter : ISorter
    {
        public string[] Text { get; set; }

        public void BubbleSort<T>(IList<T> list)
        {
            BubbleSort(list, Comparer<T>.Default);
        }

        public void BubbleSort<T>(IList<T> lines, IComparer<T> comparer)
        {
            bool active = true;
            while (active)
            {
                active = false;
                for (int line = 0; line < lines.Count - 1; line++)
                {
                    T x = lines[line];
                    T y = lines[line + 1];
                    if (comparer.Compare(x, y) > 0)
                    {
                        lines[line] = y;
                        lines[line + 1] = x;
                        active = true;
                    }
                }
            }
        }

        public IEnumerable<string> Sort()
        {
            BubbleSort(Text);

            return Text;
        }
    }
}
