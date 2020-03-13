namespace Web.Utils.Sorting.Algorithms
{

    // Uses last element as pivot
    public class QuickSort : ISortingStrategy
    {
        public void SortArray(long[] arr)
        {
            int min = 0;
            int max = arr.Length - 1;

            Sort(min, max, arr);
        }

        private void Sort(int min, int max, long[] array)
        {
            if (min < max)
            {
                int pi = Partition(min, max, array);

                Sort(min, pi - 1, array);
                Sort(pi + 1, max, array);
            }
        }

        private int Partition(int min, int max, long[] array)
        {
            long pivot = GetPivot(max, array);

            int i = min - 1;

            for (int j = min; j < max; j++)
            {
                if (array[j] < pivot)
                {
                    i++;

                    long tmp = array[j];
                    array[j] = array[i];
                    array[i] = tmp;
                }
            }

            long temp = array[max];
            array[max] = array[i + 1];
            array[i+1] = temp;

            return i + 1;
        }

        private long GetPivot(int max, long[] array)
        {
            int index = max;
            return array[index];
        }
    }
}
