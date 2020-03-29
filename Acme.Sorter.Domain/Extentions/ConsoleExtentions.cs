using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Acme.Sorter.Domain.Extentions
{
    public static class ConsoleExtentions
    {
        public static void DisplayToConsole(this IEnumerable<string> text)
        {
            text.ToList().ForEach(line =>
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine(line);
            });
        }
    }
}
