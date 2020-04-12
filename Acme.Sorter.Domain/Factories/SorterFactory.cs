using Acme.Sorter.Contracts;
using Acme.Sorter.Contracts.Models;
using Acme.Sorter.Domain.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Sorter.Domain.Factories
{
    public interface ISorterFactory
    {
        ISorterFactory WithTextContent(string[] args);
        IEnumerable<KeyValuePair<string, string>> WithSorter(SorterType sortType);
    }

    public class SorterFactory : ISorterFactory
    {
        private const string FileName = "russian_poem.txt";

        private string[] _text;
        private readonly IEnumerable<ISorter> _sorters;

        public SorterFactory(IEnumerable<ISorter> sorters)
        {
            _sorters = sorters;
        }

        private ISorter WithSorter(SorterType sortertype, string[] text)
        {
            var sorter = _sorters.SingleOrDefault(q => q.GetType().GetAttributeType().Equals(sortertype));

            if (sorter == null)
                throw new UnknownSorterException(sortertype.ToString());

            sorter.Text = text.CleanText();

            return sorter;
        }

        public ISorterFactory WithTextContent(string[] args)
        {
            _text = args.Length == 0 ? FileName.GetText() : args;

            return this;
        }

        public IEnumerable<KeyValuePair<string, string>> WithSorter(SorterType sortType)
        {
            if (sortType == SorterType.All)
            {
                var result = new List<KeyValuePair<string, string>>();
                var types = Enum.GetValues(typeof(SorterType)).Cast<SorterType>().Where(q => q != SorterType.All).ToList();

                types.ForEach(type => result.AddRange(Process(type)));

                return result;
            }

            return Process(sortType);
        }

        private IEnumerable<KeyValuePair<string, string>> Process(SorterType type)
        {
            var results = WithSorter(type, _text).Sort();
            var pairs = new List<KeyValuePair<string, string>>();

            foreach (var result in results)
            {
                pairs.Add(new KeyValuePair<string, string>(result, type.ToString()));
            }

            return pairs;
        }
    }
}
