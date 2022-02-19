using System.Collections.Generic;

namespace FrameworkFragments.MethodResponse.Store
{
    public static class StoreResponseFactory
    {
        public static IStoreResponse Saved()
        {
            //TODO: Call Trace or Logging Provider

            return new StoreResponse(
                StoreStatusType.Saved,
                CallStackTool.GetMethodPath(),
                null,
                null
            );
        }
        public static IStoreResponse Invalid(IEnumerable<IValidationFailure> validationFailures)
        {
            //TODO: Call Trace or Logging Provider

            return new StoreResponse(
                StoreStatusType.Invalid,
                CallStackTool.GetMethodPath(),
                validationFailures,
                null
            );
        }
        public static IStoreResponse Error(IEnumerable<IError> errors)
        {
            //TODO: Call Trace or Logging Provider

            return new StoreResponse(
                StoreStatusType.Error,
                CallStackTool.GetMethodPath(),
                null,
                errors
            );
        }
    }
}
