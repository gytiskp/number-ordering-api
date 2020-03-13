using System;

namespace Web.Utils.Sorting
{
    public class ArraySorter
    {
        private readonly ISortingStrategy _strategy;

        public ArraySorter(ISortingStrategy strategy)
        {
            _strategy = strategy;
        }

        public void Sort(long[] array)
        {
            _strategy.SortArray(array);
        }

    }
}
