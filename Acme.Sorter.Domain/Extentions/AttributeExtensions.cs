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
            return attribute.SorterType;
        }
    }
}
