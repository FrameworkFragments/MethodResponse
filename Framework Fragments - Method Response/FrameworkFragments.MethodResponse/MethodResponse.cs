#nullable enable
using System.Collections.Generic;
using FrameworkFragments.Error;
using FrameworkFragments.Validation;

namespace FrameworkFragments.MethodResponse
{
    internal abstract class MethodResponse
    {
        private readonly string _responseSource;
        private readonly IEnumerable<IValidationFailure>? _validationFailures;
        private readonly IEnumerable<IError>? _errors;

        protected internal MethodResponse(string responseSource, IEnumerable<IValidationFailure>? validationFailures,
            IEnumerable<IError>? errors)
        {
            _responseSource = responseSource;
            _validationFailures = validationFailures;
            _errors = errors;
        }
        
        public string GetResponseSource()
        {
            return _responseSource;
        }

        public IEnumerable<IValidationFailure>? GetValidationFailures()
        {
            return _validationFailures;
        }

        public IEnumerable<IError>? GetErrors()
        {
            return _errors;
        }
    }
}
