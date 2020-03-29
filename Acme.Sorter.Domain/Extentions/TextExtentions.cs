using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Acme.Sorter.Domain.Extentions
{
    public static class TextExtentions
    {
        public static string Clean(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("No input text provided");

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var t in normalizedString)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(t);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        [ExcludeFromCodeCoverage]
        public static string[] GetText(this string filename)
        {
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directory = Path.GetDirectoryName(location);
            var file = Path.Combine(directory, filename);

            return File.ReadAllLines(file, Encoding.UTF8); 
        }

        public static string[] CleanText(this string[] text)
        {
            var hashset = new HashSet<string>();

            text.ToList().ForEach(line => hashset.Add(string.IsNullOrWhiteSpace(line) ? string.Empty : line.Clean()));

            return hashset.ToArray();
        }
    }
}
