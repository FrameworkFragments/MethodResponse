#nullable enable
using System.Collections.Generic;
using Smelter.Common.Error;
using Smelter.Common.Validation;

namespace Smelter.Common.MethodResponse
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
