#nullable enable
using System.Collections.Generic;
using Smelter.Common.Error;
using Smelter.Common.Validation;

namespace Smelter.Common.MethodResponse
{
    internal class ValidateResponse: MethodResponse, IValidateResponse
    {
        private readonly ValidateStatusType _fetchStatusType;

        internal ValidateResponse(ValidateStatusType fetchStatusType, string responseSource, IEnumerable<IValidationFailure>? validationFailures, IEnumerable<IError>? errors)
            : base(responseSource, validationFailures, errors)
        {
            _fetchStatusType = fetchStatusType;
        }

        public bool IsValid()
        {
            return ValidateStatusType.Valid == _fetchStatusType;
        }

        public bool IsInvalid()
        {
            return ValidateStatusType.Invalid == _fetchStatusType;
        }

        public bool IsError()
        {
            return ValidateStatusType.Error == _fetchStatusType;
        }

        public override string ToString()
        {
            return GetResponseSource() + this.GetType().Name + ":" + _fetchStatusType;
        }
    }
}
