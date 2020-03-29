using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Acme.Sorter.Domain.Extentions
{
    [ExcludeFromCodeCoverage]
    public static class ConsoleExtentions
    {
        public static void DisplayToConsole(this IEnumerable<KeyValuePair<string, string>> entries)
        {
            foreach(var item in entries.GroupBy(q => q.Value))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Processed: {item.Key}");
                Console.ResetColor();
                Console.OutputEncoding = Encoding.UTF8;

                item.ToList().ForEach(q => Console.WriteLine(q.Key));
                
                Console.WriteLine();
            }
        }
    }
}
