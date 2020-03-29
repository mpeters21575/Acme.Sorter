using Acme.Sorter.Contracts;
using Acme.Sorter.Domain.Extentions;
using Acme.Sorter.Domain.Factories;
using Acme.Sorter.Domain.Strategies;
using StructureMap;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Sorter.Domain
{
    public static class WithBootstrapper
    {
        private static IContainer Bootup()
        {
            var container = new Container();

            container.Configure(config =>
            {
                config.For<ISorterFactory>().Use<SorterFactory>().Transient();

                config.For<ISorter>().AddInstances(q =>
                {
                    q.Type<AlphabeticalSorter>();
                    q.Type<BubbleSorter>();
                    q.Type<LengthSorter>();
                });
            });

            return container;
        }

        public static ISorterFactory WithSorterFactoryInstance()
        {
            return Bootup().GetInstance<ISorterFactory>();
        }

        public static IEnumerable<ISorter> WithSorters(Contracts.Models.SorterType sorterType = Contracts.Models.SorterType.All)
        {
            var sorters = Bootup().GetAllInstances<ISorter>();
            var sorter = sorters.SingleOrDefault(q => q.GetType().GetAttributeType() == sorterType);

            return sorterType == Contracts.Models.SorterType.All ? sorters : new List<ISorter> { sorter };
        }
    }
}
