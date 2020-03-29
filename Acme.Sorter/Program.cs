using Acme.Sorter.Contracts.Models;
using Acme.Sorter.Domain;
using Acme.Sorter.Domain.Extentions;

namespace Acme.Sorter
{
    static class Program
    {
        static void Main(string[] args)
        {
            WithBootstrapper
                 .WithSorterFactoryInstance()
                 .WithTextContent(args)
                 .WithSorter(SorterType.All)
                 .DisplayToConsole();
        }
    }
}
