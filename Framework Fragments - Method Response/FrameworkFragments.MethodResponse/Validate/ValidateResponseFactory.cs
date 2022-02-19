using System.Collections.Generic;
using Smelter.Common.Error;
using Smelter.Common.Tool;
using Smelter.Common.Validation;

namespace Smelter.Common.MethodResponse
{
    public static class ValidateResponseFactory
    {
        public static IValidateResponse Valid()
        {
            //TODO: Call Trace or Logging Provider

            return new ValidateResponse(
                ValidateStatusType.Valid,
                CallStackTool.GetMethodPath(),
                null,
                null
            );
        }
        public static IValidateResponse Invalid(IEnumerable<IValidationFailure> validationFailures)
        {
            //TODO: Call Trace or Logging Provider

            return new ValidateResponse(
                ValidateStatusType.Invalid,
                CallStackTool.GetMethodPath(),
                validationFailures,
                null
            );
        }
        public static IValidateResponse Error(IEnumerable<IError> serviceErrors)
        {
            //TODO: Call Trace or Logging Provider

            return new ValidateResponse(
                ValidateStatusType.Error,
                CallStackTool.GetMethodPath(),
                null,
                serviceErrors
            );
        }
    }
}
