using Acme.Sorter.Contracts;
using Acme.Sorter.Domain.Extentions;
using Acme.Sorter.Domain.Factories;
using Acme.Sorter.Domain.Strategies;
using SimpleInjector;
using System.Collections.Generic;
using System.Linq;

namespace Acme.Sorter.Domain
{
    public static class WithBootstrapper
    {
        private static Container Bootup()
        {
            var container = new Container();
            container.Collection.Register<ISorter>(new AlphabeticalSorter(), new BubbleSorter(), new LengthSorter());
            container.Register<ISorterFactory, SorterFactory>();
            container.Verify();

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
