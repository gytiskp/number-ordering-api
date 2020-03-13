using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Web.Utils;

namespace Web.Models
{
    public class SortingInput : IValidatableObject
    {
        [Required]
        public string Line { get; set; }

        public long[] GetNumbers()
        {
            return Array.ConvertAll(Line.Trim().Split(ConstantHelper.NUMBER_SEPARATOR), long.Parse);
        }

        #region model validation

        private const int MIN_NUM_VAL = 0;
        private const int MAX_NUM_VAL = 10;
        private const int MAX_ARR_LENGTH = 10;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            bool correctPattern = OnlyNumbersAndSpaces(errors, this);
            if (!correctPattern) return errors;

            ArrayIsCorrectLength(errors, this, MAX_ARR_LENGTH);
            ArrayValuesBetweenMinMax(errors, this, MIN_NUM_VAL, MAX_NUM_VAL);
            ArrayHasNoDuplicates(errors, this);

            return errors;
        }

       

        private bool OnlyNumbersAndSpaces(List<ValidationResult> errors, SortingInput input)
        {
            string pattern = @"^([1-9][0-9]*[ ])*[1-9][0-9]*[ ]*$";

            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(input.Line))
            {
                errors.Add(new ValidationResult($"Only numbers separated by space are allowed. Ex: \"4 2 3 8\"."));
                return false;
            }

            return true;
        }

        private void ArrayIsCorrectLength(List<ValidationResult> errors, SortingInput input, int maxArrLength)
        {
            long[] arr = input.GetNumbers();

            if (arr.Length > maxArrLength)
            {
                errors.Add(new ValidationResult($"Max of {maxArrLength} numbers is allowed.", new[] { "Line" }));
            }
        }

        private void ArrayValuesBetweenMinMax(List<ValidationResult> errors, SortingInput input, int min, int max)
        {
            long[] arr = input.GetNumbers();
            Array.Sort(arr);

            if (arr.Any(f => f < min) || arr.Any(f => f > max))
            {
                errors.Add(new ValidationResult($"Numbers must be between {min} and {max}.", new[] { "Line" }));
            }
        }

        private void ArrayHasNoDuplicates(List<ValidationResult> errors, SortingInput input)
        {
            long[] arr = input.GetNumbers();

            if (arr.GroupBy(f => f).Any(g => g.Count() > 1))
            {
                errors.Add(new ValidationResult($"Sequence cannot have duplicate numbers.", new[] { "Line" }));
            }
        }
        
        #endregion
    }
}
