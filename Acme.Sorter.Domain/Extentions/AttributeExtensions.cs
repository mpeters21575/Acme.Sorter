using Acme.Sorter.Contracts;
using Acme.Sorter.Contracts.Models;
using System;
using System.Linq;

namespace Acme.Sorter.Domain.Extentions
{

    public static class AttributeExtensions
    {
        public static SorterType GetAttributeType(this Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(SorterTypeAttribute), true).FirstOrDefault() as SorterTypeAttribute;

            if (attribute == null) throw new UnknownSorterException();

            return attribute.SorterType;
        }
    }
}
