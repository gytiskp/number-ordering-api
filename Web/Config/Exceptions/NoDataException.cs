using System;
using Web.Utils;

namespace Web.Config.Exceptions
{
    public class NoDataException : Exception
    {
        public NoDataException() : base(ConstantHelper.NO_DATA_EX)
        {

        }
    }
}
