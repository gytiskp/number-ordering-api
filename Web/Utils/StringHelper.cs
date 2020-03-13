using System.Text;

namespace Web.Utils
{
    public static class StringHelper
    {
        public static string Concat(long[] array)
        {
            if (array == null)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < array.Length - 1; i++)
            {
                sb.Append(array[i]).Append(ConstantHelper.NUMBER_SEPARATOR);
            }

            sb.Append(array[array.Length - 1]);

            return sb.ToString();
        }
    }
}
