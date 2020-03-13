namespace Web.Utils.Sorting.Algorithms
{
    public class MergeSort : ISortingStrategy
    {
        public void SortArray(long[] arr)
        {
            int first = 0;
            int last = arr.Length - 1;

            Sort(first, last, arr);
        }

        public void Sort(int left, int right, long[] arr)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                Sort(left, middle, arr);
                Sort(middle + 1, right, arr);

                Merge(left, middle, right, arr);
            }
        }

        void Merge(int left, int middle, int right, long[] arr)
        {
            int i;
            int j;

            int n1 = middle - left + 1;
            int n2 = right - middle;

            long[] L = new long[n1];
            long[] R = new long[n2];

            for (i = 0; i < n1; i++)
            {
                L[i] = arr[left + i];
            }

            for (j = 0; j < n2; j++)
            {
                R[j] = arr[middle + 1 + j];
            }

            i = j = 0;
            int k = left;

            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }

        }
    }
}
