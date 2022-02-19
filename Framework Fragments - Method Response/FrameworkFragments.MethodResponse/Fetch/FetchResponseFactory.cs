using System.Collections.Generic;

namespace FrameworkFragments.MethodResponse.Fetch
{
    public static class FetchResponseFactory
    {
        public static IFetchResponse Found()
        {
            //TODO: Call Trace or Logging Provider

            return new FetchResponse(
                FetchStatusType.Found,
                CallStackTool.GetMethodPath(),
                null,
                null
            );
        }
        public static IFetchResponse NotFound()
        {
            //TODO: Call Trace or Logging Provider

            return new FetchResponse(
                FetchStatusType.NotFound,
                CallStackTool.GetMethodPath(),
                null,
                null
            );
        }
        public static IFetchResponse Invalid(IEnumerable<IValidationFailure> validationFailures)
        {
            //TODO: Call Trace or Logging Provider

            return new FetchResponse(
                FetchStatusType.Invalid,
                CallStackTool.GetMethodPath(),
                validationFailures,
                null
            );
        }
        public static IFetchResponse Error(IEnumerable<IError> errors)
        {
            //TODO: Call Trace or Logging Provider

            return new FetchResponse(
                FetchStatusType.Invalid,
                CallStackTool.GetMethodPath(),
                null,
                errors
            );
        }
    }
}
