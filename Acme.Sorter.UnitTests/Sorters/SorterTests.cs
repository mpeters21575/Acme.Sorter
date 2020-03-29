using Acme.Sorter.Contracts;
using Acme.Sorter.Domain;
using Acme.Sorter.Domain.Factories;
using StructureMap;
using System;
using System.Linq;
using Xunit;

namespace Acme.Sorter.UnitTests
{
    public class SorterTests
    {
        private readonly SorterFactory _factory;
        private readonly IContainer _container;

        public SorterTests()
        {
            _container = Bootstrapper.Bootup();
            _factory = new SorterFactory(_container);
        }

        [Theory]
        [InlineData(Contracts.Models.SorterType.Alphabetically, new[] { "D","A","C","B"}, new[] { "A", "B", "C", "D" })]
        [InlineData(Contracts.Models.SorterType.TextLength, new[] { "AAAAA", "AA", "A", "AAAAAA" }, new[] { "A", "AA", "AAAAA", "AAAAAA" })]
        [InlineData(Contracts.Models.SorterType.BubbleSort, new[] { "D", "A", "C", "B" }, new[] { "A", "B", "C", "D" })]
        public void TestAlphabeticalSorter(Contracts.Models.SorterType sorterType, string [] text, string [] expectedResult)
        {
            var sorter = _factory.Create(sorterType, text);
            var result = sorter.Sort();

            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData(3)]
        public void TestSorterCount(int expectedCount)
        {
            var sorters = _container.GetAllInstances<ISorter>();

            Assert.Equal(expectedCount, sorters.Count());
        }

        [Theory]
        [InlineData(Contracts.Models.SorterType.Alphabetically, new[] { "D", "A", "C", "B" })]
        public void TestNoSorters(Contracts.Models.SorterType sorterType, string[] text)
        {
            var container = new Container();
            var factory = new SorterFactory(container);

            try
            {
                var sorter = factory.Create(sorterType, text);
            }
            catch (Exception exception)
            {
                Assert.IsType<NullReferenceException>(exception);
            }
        }

        [Theory]
        [InlineData(Contracts.Models.SorterType.None, new[] { "D", "A", "C", "B" })]
        public void TestInvalidSorter(Contracts.Models.SorterType sorterType, string[] text)
        {
            try
            {
                var sorter = _factory.Create(sorterType, text);
                var result = sorter.Sort();
            }
            catch (Exception exception)
            {
                Assert.IsType<ArgumentOutOfRangeException>(exception);
            }

        }
    }
}
