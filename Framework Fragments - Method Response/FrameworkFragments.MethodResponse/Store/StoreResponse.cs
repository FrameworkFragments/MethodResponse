#nullable enable
using System.Collections.Generic;
using Smelter.Common.Error;
using Smelter.Common.Validation;

namespace Smelter.Common.MethodResponse
{
    internal class StoreResponse: MethodResponse, IStoreResponse
    {
        private readonly StoreStatusType _statusType;

        internal StoreResponse(StoreStatusType statusType, string responseSource, IEnumerable<IValidationFailure>? validationFailures, IEnumerable<IError>? errors) 
        : base(responseSource, validationFailures, errors)
        {
            _statusType = statusType;
        }

        public bool IsSaved()
        {
            return StoreStatusType.Saved == _statusType;
        }
        public bool IsInvalid()
        {
            return StoreStatusType.Invalid == _statusType;
        }
        public bool IsError()
        {
            return StoreStatusType.Error == _statusType;
        }
    }
}
