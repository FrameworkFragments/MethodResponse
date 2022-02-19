#nullable enable
using System.Collections.Generic;
using Smelter.Common.Error;
using Smelter.Common.Validation;

namespace Smelter.Common.MethodResponse
{
    internal class FetchResponse : MethodResponse, IFetchResponse
    {
        private readonly FetchStatusType _fetchStatusType;

        internal FetchResponse(FetchStatusType fetchStatusType, string responseSource, IEnumerable<IValidationFailure>? validationFailures, IEnumerable<IError>? errors)
            : base(responseSource, validationFailures, errors)
        {
            _fetchStatusType = fetchStatusType;
        }

        public bool IsFound()
        {
            return FetchStatusType.Found == _fetchStatusType;
        }
        public bool IsNotFound()
        {
            return FetchStatusType.NotFound == _fetchStatusType;
        }
        public bool IsInvalid()
        {
            return FetchStatusType.Invalid == _fetchStatusType;
        }
        public bool IsError()
        {
            return FetchStatusType.Error == _fetchStatusType;
        }

        public override string ToString()
        {
            return GetResponseSource() + this.GetType().Name + ":" + _fetchStatusType;
        }
    }
}
