#nullable enable
using System.Collections.Generic;
using FrameworkFragments.Error;
using FrameworkFragments.Validation;

namespace FrameworkFragments.MethodResponse
{
    public interface IMethodResponse
    {
        public bool IsInvalid();
        public bool IsError();
        public string GetResponseSource();

        public IEnumerable<IValidationFailure>? GetValidationFailures();

        public IEnumerable<IError>? GetErrors();
    }
}
