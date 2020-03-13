using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Tests.Utils
{
    public static class CommonHelper
    {
        public static List<ValidationResult> GetValidationErrors(IValidatableObject obj)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);
            Validator.TryValidateObject(obj, validationContext, result);
            obj.Validate(validationContext);

            return result;
        }
    }
}
