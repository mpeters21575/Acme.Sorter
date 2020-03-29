using Acme.Sorter.Contracts;
using Acme.Sorter.Domain.Strategies;
using StructureMap;

namespace Acme.Sorter.Domain
{
    public static class Bootstrapper
    {
        public static IContainer Bootup()
        {
            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssembliesAndExecutablesFromApplicationBaseDirectory();
                    _.WithDefaultConventions();
                });

                config.For<ISorter>().AddInstances(q =>
                {
                    q.Type<AlphabeticalSorter>();
                    q.Type<BubbleSorter>();
                    q.Type<LengthSorter>();
                });
            });

            return container;
        }
    }
}
