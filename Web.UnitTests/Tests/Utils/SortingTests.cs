using System;
using System.Collections;
using System.Collections.Generic;
using Web.Utils.Sorting;
using Web.Utils.Sorting.Algorithms;
using Xunit;

namespace Web.Tests.Tests.Utils
{
    public class SortingTests
    {
        private const int RNG_ARRAY_LENGTH = 1_000_000;

        [Theory]
        [ClassData(typeof(SortTestData))]
        public void QuickSort_smallArrays(long[] unsortedArray)
        {
            Sort(new QuickSort(), unsortedArray, out long[] manuallySorted, out long[] dotnetSorted);

            Assert.Equal(dotnetSorted, manuallySorted);
        }

        [Theory]
        [ClassData(typeof(SortTestData))]
        public void MergeSort_smallArrays(long[] unsortedArray)
        {
            Sort(new MergeSort(), unsortedArray, out long[] manuallySorted, out long[] dotnetSorted);

            Assert.Equal(dotnetSorted, manuallySorted);
        }


        [Fact]
        public void QuickSort_largeArray()
        {
            long[] unsortedArray = GetRandomGeneratedArray(RNG_ARRAY_LENGTH);

            Sort(new QuickSort(), unsortedArray, out long[] manuallySorted, out long[] dotnetSorted);

            Assert.Equal(dotnetSorted, manuallySorted);
        }

        [Fact]
        public void MergeSort_largeArray()
        {
            long[] unsortedArray = GetRandomGeneratedArray(RNG_ARRAY_LENGTH);

            Sort(new MergeSort(), unsortedArray, out long[] manuallySorted, out long[] dotnetSorted);

            Assert.Equal(dotnetSorted, manuallySorted);
        }

        private long[] GetRandomGeneratedArray(int length)
        {
            long[] arr = new long[length];

            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                arr[i] = random.Next(0, length);
            }

            return arr;
        }

        private void Sort(ISortingStrategy algorithm, long[] unsortedArray, out long[] manuallySorted, out long[] dotnetSorted)
        {
            manuallySorted = (long[])unsortedArray.Clone();
            dotnetSorted = (long[])unsortedArray.Clone();

            ArraySorter sorter = new ArraySorter(algorithm);
            sorter.Sort(manuallySorted);

            Array.Sort(dotnetSorted);
        }

    }

    class SortTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { new long[] { } },                            // empty array
            new object[] { new long[] { 5 } },                          // one element array
            new object[] { new long[] { 5, 2, 9, 1, -15, 8, 3 } },      // unsorted array
            new object[] { new long[] { -1, 2, 4, 7, 10 } },            // sorted array
            new object[] { new long[] { 5, 2, 9, -15, 1, 1, -15, 8, 2, 3 } },     // unsorted array with duplicates
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

