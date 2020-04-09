using System;

namespace Acme.Sorter.Contracts.Models
{

    [Serializable]
    public class UnknownSorterException : Exception
    {
        public UnknownSorterException() : base()
        {
        }

        public UnknownSorterException(string message) : base($"Unknown sorter '{message}'")
        {
        }

        public UnknownSorterException(string message, Exception innerException) : base($"Unknown sorter '{message}'", innerException)
        {
        }
    }
}
