using Acme.Sorter.Contracts.Models;
using System;

namespace Acme.Sorter.Domain
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SorterTypeAttribute : Attribute
    {
        public SorterType SorterType { get; set; }
    }
}
