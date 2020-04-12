using Acme.Sorter.Contracts.Models;
using Acme.Sorter.Domain;
using Acme.Sorter.UnitTests.Fakes;
using System;
using System.Linq;
using Xunit;

namespace Acme.Sorter.UnitTests
{
    public class WhenTestingSorters
    {
        [Theory]
        [InlineData(SorterType.Alphabetically, new[] { "D", "A", "C", "B" }, new[] { "A", "B", "C", "D" })]
        [InlineData(SorterType.TextLength, new[] { "AAAAA", "AA", "A", "AAAAAA" }, new[] { "A", "AA", "AAAAA", "AAAAAA" })]
        [InlineData(SorterType.BubbleSort, new[] { "D", "A", "C", "B" }, new[] { "A", "B", "C", "D" })]
        public void ThenTheSortersShouldSucceed(Contracts.Models.SorterType sorterType, string[] text, string[] expectedResult)
        {
            var result = WithBootstrapper
                .WithSorterFactoryInstance()
                .WithTextContent(text)
                .WithSorter(sorterType);

            Assert.Equal(result.Select(q => q.Key), expectedResult);
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
                WithBootstrapper
                    .WithSorterFactoryInstance()
                    .WithTextContent(null)
                    .WithSorter(sorterType);

            }
            catch (Exception exception)
            {
                Assert.IsType<NullReferenceException>(exception);
            }
        }

        [Theory]
        [InlineData(FakeSorterType.Shest, new[] { "D", "A", "C", "B" })]
        public void ThenInvalidSortersShouldFail(FakeSorterType sorterType, string[] text)
        {
            try
            {
                var result = WithBootstrapper
                    .WithSorterFactoryInstance()
                    .WithTextContent(text)
                    .WithSorter((SorterType)sorterType);
            }
            catch (Exception exception)
            {
                Assert.IsType<UnknownSorterException>(exception);
            }
        }
    }
}
