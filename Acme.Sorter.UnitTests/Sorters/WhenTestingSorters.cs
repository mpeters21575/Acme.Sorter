using Acme.Sorter.Domain;
using System;
using System.Linq;
using Xunit;

namespace Acme.Sorter.UnitTests
{
    public class WhenTestingSorters
    {
        [Theory]
        [InlineData(Contracts.Models.SorterType.Alphabetically, new[] { "D", "A", "C", "B" }, new[] { "A", "B", "C", "D" })]
        [InlineData(Contracts.Models.SorterType.TextLength, new[] { "AAAAA", "AA", "A", "AAAAAA" }, new[] { "A", "AA", "AAAAA", "AAAAAA" })]
        [InlineData(Contracts.Models.SorterType.BubbleSort, new[] { "D", "A", "C", "B" }, new[] { "A", "B", "C", "D" })]
        public void ThenTheAlphabeticalSorterShouldSucceed(Contracts.Models.SorterType sorterType, string[] text, string[] expectedResult)
        {
            var result = WithBootstrapper
                .WithSorterFactoryInstance()
                .WithTextContent(text)
                .WithSorter(sorterType);

            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData(3)]
        public void ThenThereShouldBeThreeSortersRegistered(int expectedCount)
        {
            var sorters = WithBootstrapper
               .WithSorters();

            Assert.Equal(expectedCount, sorters.Count());
        }

        [Theory]
        [InlineData(Contracts.Models.SorterType.Alphabetically)]
        public void ThenThereShouldBeAtLeastOneSorter(Contracts.Models.SorterType sorterType)
        {
            try
            {
                var sorter = WithBootstrapper
                    .WithSorters(sorterType)
                    .FirstOrDefault();
            }
            catch (Exception exception)
            {
                Assert.IsType<NullReferenceException>(exception);
            }
        }

        [Theory]
        [InlineData(Contracts.Models.SorterType.All, new[] { "D", "A", "C", "B" })]
        public void ThenInvalidSortersShouldFail(Contracts.Models.SorterType sorterType, string[] text)
        {
            try
            {
                var result = WithBootstrapper
                    .WithSorterFactoryInstance()
                    .WithTextContent(text)
                    .WithSorter(sorterType);
            }
            catch (Exception exception)
            {
                Assert.IsType<ArgumentOutOfRangeException>(exception);
            }
        }
    }
}
