using Acme.Sorter.Contracts;
using Acme.Sorter.Contracts.Models;
using Acme.Sorter.Domain.Extentions;
using Acme.Sorter.Domain.Strategies;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Sorter.Domain.Factories
{
    public interface ISorterFactory
    {
        ISorterFactory WithTextContent(string[] args);
        IEnumerable<string> WithSorter(SorterType sortType);
    }

    public class SorterFactory : ISorterFactory
    {
        private readonly IContainer _container;
        private string[] _text;

        public SorterFactory(IContainer container)
        {
            _container = container;
        }

        private ISorter Create(SorterType sortertype, string[] text)
        {
            var sorter = default(ISorter);
            var sorters = _container.GetAllInstances<ISorter>();

            if (sorters == null || sorters.Count() == 0) throw new NullReferenceException("No sorters registered");

            switch (sortertype)
            {
                case SorterType.BubbleSort:
                    sorter = sorters.Single(q => q.GetType() == typeof(BubbleSorter));
                    break;
                case SorterType.Alphabetically:
                    sorter = sorters.Single(q => q.GetType() == typeof(AlphabeticalSorter));
                    break;
                case SorterType.TextLength:
                    sorter = sorters.Single(q => q.GetType() == typeof(LengthSorter));
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unknown sorter '{sortertype}'");
            }

            sorter.Text = text.CleanText();

            return sorter;
        }

        public ISorterFactory WithTextContent(string[] args)
        {
            _text = args.Length == 0 ? "russian_poem.txt".GetText() : args;

            return this;
        }

        public IEnumerable<string> WithSorter(SorterType sortType)
        {
            if (sortType == SorterType.All)
            {
                var result = new List<string>();
                var types = Enum.GetValues(typeof(SorterType)).Cast<SorterType>().Where(q => q != SorterType.All).ToList();

                types.ForEach(type => result.AddRange(Process(type)));

                return result;
            }

            return Process(sortType);
        }

        private IEnumerable<string> Process(SorterType type)
        {
            var sorter = Create(type, _text);
            var results = sorter.Sort().ToList();

            results.Insert(0, $"Processed '{type}'...");
            results.Add(string.Empty);

            return results;
        }
    }
}
