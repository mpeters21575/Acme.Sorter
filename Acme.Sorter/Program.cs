using Acme.Sorter.Contracts.Models;
using Acme.Sorter.Domain;
using Acme.Sorter.Domain.Factories;
using System;
using System.Linq;
using System.Text;

namespace Acme.Sorter
{
    static class Program
    {
        static void Main(string[] args)
        {
            var container = Bootstrapper.Bootup();
            var factory = container.GetInstance<ISorterFactory>();
            var text = args.Length == 0 ? "russian_poem.txt".GetText() : args;
            var types = Enum.GetValues(typeof(SorterType)).Cast<SorterType>().Where(q => q != SorterType.None);

            foreach (var type in types)
            {
                var sorter = factory.Create(type, text);
                var results = sorter.Sort();

                Console.WriteLine($"Processed '{type}'...");

                results.ToList().ForEach(line =>
                {
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine(line);
                });

                Console.WriteLine();
            }
        }
    }
}
