using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace odata.Extensions
{
    internal static class ValidatorExtensions
    {
        internal static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}