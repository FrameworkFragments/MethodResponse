using System.Collections.Generic;

namespace FrameworkFragments.MethodResponse.Validate
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
