using Acme.Sorter.Contracts;
using Acme.Sorter.Contracts.Models;
using Acme.Sorter.Domain.Strategies;
using StructureMap;
using System;
using System.Linq;

namespace Acme.Sorter.Domain.Factories
{
    public interface ISorterFactory
    {
        ISorter Create(SorterType sortertype, string [] text);
    }

    public class SorterFactory : ISorterFactory
    {
        private readonly IContainer _container;

        public SorterFactory(IContainer container)
        {
            _container = container;
        }

        public ISorter Create(SorterType sortertype, string [] text)
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
    }
}
