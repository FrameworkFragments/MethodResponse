# Motivations

## Try-Catch == GOTO
Try-Catch is functionally equivalent to using a GOTO statement.
For the same reasons we do not use GOTO, we should minimize the use of Try-Catch to only two locations.

### Catch Exceptions At the Source
A Try Catch should wrap any single statement that can result in an exception.
Only exceptions that can be anticipated at the time of implementation should be handled at this location.
When an exception is caught, the method should return an IMethodResponse where the Outcome property is Error, and the MessageCodes property contains constant string used to identify where the exception happened and why.
Any other exception should roll up to the global exception handler.

### Global Exception Handler
Every application must implement a global exception handler.
This handler should respond to any exception that developer was not able to predict at the time of implementing a call that could result in an exception.

The result of calling a method should be crystal clear. Try-Catch behaviors

## Non-Binary Outcomes
Methods frequently must indicate more than success or failure.

For example, a factory method that accepts parameters could reach three outcomes.
1) Object created successfully
2) Object not created due to invalid configuration setting (IE: HttpClient requires a well formed URL)
3) Object not created due to system error (IE: Database unreachable)

Using a Try-Catch to handle these alternative outcomes is painfully verbose and leads to less obvious code execution paths.

This is triple true when the `try-catch` is in the web controller, but the call was made to the Service Layer, which called Data Access where the exception was encountered.

## Try-Catch Nesting
Just don't... please... just.... please don't do this. If Alan Turing was alive today, it would probably make him cry to see us so far from the rational purity of what he envisioned.

Why? Oh... because a try-catch tells the process to remember application state prior to the block. That way when an error occurs, it rolls back to that state.

Now... Remember when you opened that Try-Catch in the controller? Then another one in the service layer? Oh, and don't forget the ones down at the DataAccess level...

Seriously... we can be better than this. 

# Benefits of Adopting the Method Response Pattern
When examining a call to a non-private method within the same code base, the developer should be 100% confident that a try catch is not required because that method implements the MethodResponse pattern. 

# Example Usage
The following is pseudo code to convey how multi-object interactions are more clear to the caller when not relying on Try-Catch behavior for complex outcomes. 

It also includes the optional use of Message Codes, which are one way this pattern can be helpful both for logging out comes, and invoking UI logic to explain failures to users.
```c#

namespace Company.Application.MethodResponses {
    public enum CommonOutcomeType : ushort
    {
      Undefined = 0,
      Success = 1,
      Invalid = 2,
      Error = 3,
    }
    
    public record ValidateMethodResponse: MethodResponseBase<ValidateMethodResponse.OutcomeType>
    {
      public enum OutcomeType : ushort
      {
        Undefined = CommonOutcomeType.Undefined,
        Valid = CommonOutcomeType.Success,
        Invalid = CommonOutcomeType.Invalid,
        Error = CommonOutcomeType.Error,
      }
    }
    
    public record InitializeMethodResponse: MethodResponseBase<InitializeMethodResponse.OutcomeType>
    {
      public enum OutcomeType : ushort
      {
        Undefined = CommonOutcomeType.Undefined,
        Initialized = CommonOutcomeType.Success,
        Invalid = CommonOutcomeType.Invalid,
        Error = CommonOutcomeType.Error,
      }
    }
}

namespace Company.Application.Services {
    public class XyzFactory
    {
        public static class MessageCodes {
            public const string InvalidApiUri = "INVALID_API_URI";
            public const string ApiRequestInvalid = "API_REQUEST_INVALID";
            public const string ApiRateLimit = "API_RATE_LIMIT";
            public const string ApiServerError = "API_SERVER_ERROR";
        }
        public ValidateMethodResponse ValidateXyzConfiguration(IXyzConfiguration xyzConfiguration)
        {
            if (String.Empty(xyzConfiguration.ApiUri)) {
                return new ValidateMethodResponse(ValidateMethodResponse.OutcomeType.Invalid, MessageCodes.InvalidApiUri);
            }
            
            return new ValidateMethodResponse(ValidateMethodResponse.OutcomeType.Valid);
        }
    
        public (InitializeMethodResponse, IXyzClient?) InitializeXyz(IXyzConfiguration xyzConfiguration)
        {
            var validationResponse = ValidateXyzConfiguration(xyzConfiguration);
            if (validationResponse.IsNot(ValidateMethodResponse.OutcomeType.Valid) {
                return (new InitializeMethodResponse(ValidateMethodResponse.OutcomeType.Invalid, validationResponse.MessageCodes), null); 
            }
    
            try {
                // call an API to get additional config information
            } catch (HttpRequestException httpRequestEx) {
                if (xyzClientExtensions.IsRateLimitResponse(httpRequestEx)) {
                    return (new InitializeMethodResponse(ValidateMethodResponse.OutcomeType.Invalid, new [] {MessageCodes.ApiRateLimit, xyzClientExtensions.GetErrorMessage(httpRequestEx)}), null);
                }
                return (new InitializeMethodResponse(ValidateMethodResponse.OutcomeType.Invalid, new [] {MessageCodes.ApiRequestInvalid, xyzClientExtensions.GetErrorMessage(httpRequestEx)}), null);
            } catch (HttpServerException httpServerEx) {
                return (new InitializeMethodResponse(ValidateMethodResponse.OutcomeType.Error, new [] {MessageCodes.ApiServerError, xyzClientExtensions.GetErrorMessage(httpServerEx)}), null);
            }
            
            result = new XyzClient() {
             // configure From configuration and API response
            };
            
            return (new InitializeMethodResponse(ValidateMethodResponse.OutcomeType.Initialized, xyzClient);
        }
    }
}
namespace Company.Application.Integrations {
    public class XyzClient
    {
        public record ComplexStuffMethodResponse: MethodResponseBase<InitializeMethodResponse.OutcomeType>
        {
          public enum OutcomeType : ushort
          {
            Undefined = CommonOutcomeType.Undefined,
            CompleteSuccess = CommonOutcomeType.Success,
            PartialSuccessA,
            PartialSuccessB,
            InsuficientPermissions
          }
        }
        
        public MessageCodes {
            public const string StepAFailed = "STEP_A_FAILED";
            public const string StepBFailed = "STEP_B_FAILED";
            public const string StepCFailed = "STEP_C_FAILED";
        }
        
        public ComplexStuffMethodResponse ComplexStuff() {
            if (/* A fails*/) {
                return new ComplexStuffMethodResponse(ComplexStuffMethodResponse.Error, MessageCodes.StepAFailed);
            }
    
            if (/* B fails*/) {
                return new ComplexStuffMethodResponse(ComplexStuffMethodResponse.PartialSuccessA, MessageCodes.StepBFailed);
            }
            
            if (/* C fails*/) {
                return new ComplexStuffMethodResponse(ComplexStuffMethodResponse.PartialSuccessB, MessageCodes.StepCFailed);
            }
            return new ComplexStuffMethodResponse(ComplexStuffMethodResponse.CompleteSuccess);
        }
    }
}

namespace Company.Application {
    public class Program
    {
        public void Main()
        {
            var xyzConfiguration = new XyzConfiguration();
            var xyzFactory = new XyzFactory();
            
            var (initializeMethodResponse, xyzClient) = XyzFactory.InitializeXyz(xyzConfiguration);
            if (initializeMethodResponse.IsNot(ValidateMethodResponse.OutcomeType.Initialized)) {
                Console.WriteLine("Failed to init XyzClient");
                methodResponse.MessageCodes.Each(Console.WriteLine);
                return;
            }
            
            var complexStuffMethodResponse = xyzClient.ComplexStuff();
            if (complexStuffMethodResponse.Is(ComplexStuffMethodResponse.CompleteSuccess) {
                Console.WriteLine("XyzClient completed successfully.");
                return;
            }
            Console.WriteLine("XyzClient was NOT complete successful.");
            
            switch (complexStuffMethodResponse.Outcome) {
                case ComplexStuffMethodResponse.PartialSuccessA:
                    Console.WriteLine("Only Step A succeeded, queuing work to retry steps B & C.");
                    // do case specific stuff, like queue up a retry of step B&C only
                    return;
                case ComplexStuffMethodResponse.PartialSuccessB:
                    Console.WriteLine("Only Step A succeeded, queuing work to retry step C only.");
                    // do case specific stuff, like queue up a retry of step C only
                    return;
                default:
                    Logger.Emergency("XyzClient failed for an unexpected reason. Please investigate for case XXXXXXX!");
                    Console.WriteLine("Unable to determine XyzClient's outcome!!\nEmergency level log created!");
                    return;
            }
        }
    }
}
```