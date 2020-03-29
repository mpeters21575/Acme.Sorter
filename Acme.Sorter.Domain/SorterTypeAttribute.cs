using Acme.Sorter.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Acme.Sorter.Domain
{

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SorterTypeAttribute : Attribute
    {
        public SorterType SorterType { get; set; }
    }
}
