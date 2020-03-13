using Web.Models;
using Web.Tests.Utils;
using Xunit;

namespace Web.Tests.Tests.Models
{
    public class ModelValidationTests
    {
        [Theory]
        [InlineData("invalid string", false)]           // not numbers
        [InlineData("", false)]                         // empty
        [InlineData("31", false)]                       // value > 10
        [InlineData("-1", false)]                       // value < 0
        [InlineData("1 2 3 4 5 6 7 8 9 10 11", false)]  // length > 10
        [InlineData("1 2 3 3", false)]                  // contains duplicates
        [InlineData("1 2 3 b 5 6 7 8 9 10 11", false)]  // invalid string
        [InlineData("1-2 3 4", false)]                  // invalid string
        [InlineData("4 2 ", true)]                      // trailing space
        [InlineData("4  2 ", false)]
        [InlineData("4 2 1 3 7 6", true)]
        [InlineData("4 2 1 3 9 8 5 10 7 6", true)]

        public void ValidateModelProperties(string input, bool expected)
        {
            SortingInput model = new SortingInput { Line = input };

            var errors = CommonHelper.GetValidationErrors(model);
            var valid = errors.Count == 0;

            Assert.Equal(expected, valid);
        }

    }
}
