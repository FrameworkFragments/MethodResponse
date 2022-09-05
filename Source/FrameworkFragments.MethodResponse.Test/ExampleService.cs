using System.Reflection;
using FrameworkFragments.ErrorInfo;
using FrameworkFragments.Validation;
using FrameworkFragments.Validation.Result;
using Validator = FrameworkFragments.Validation.Validator;

namespace FrameworkFragments.MethodResponse.Test;

public class AtomicUpdateResponse : MethodResponse<AtomicUpdateResponse.OutcomeType>
{
  public enum OutcomeType : ushort
  {
    Error = 1,
    ChangesApplied = 2,
    NoChanges = 3
  }

  private AtomicUpdateResponse(OutcomeType outcome, IErrorInfo? errorInfo)
    : base(outcome, errorInfo)
  {
  }

  public static implicit operator AtomicUpdateResponse(OutcomeType outcomeType) => new(outcomeType, null);
  public static implicit operator AtomicUpdateResponse(ErrorInfo.ErrorInfo errorInfo) => new(OutcomeType.Error, errorInfo);
}

public record ProfileVo(string Name)
{
  public string Name { get; set; } = Name;
}

public class ExampleService
{
  public AtomicUpdateResponse UpdateProfile(ProfileVo profileVo)
  {
    var validationResults = new Validator()
      .Add(() => ValidateProfile(profileVo))
      .Validate();

    if (validationResults.HasFailures)
    {
      var validationError = new ErrorInfo.ErrorInfo("UPDATE_PROFILE_VALIDATION", "Profile validation failed.",
        validationResults);
      return validationError;
    }

    var rowsAffected = 0;
    try
    {
      rowsAffected++;
    }
    catch (Exception exception)
    {
      return new ErrorInfo.ErrorInfo("X_DIED", exception.Message);
    }

    if (0 < rowsAffected)
    {
      return AtomicUpdateResponse.OutcomeType.ChangesApplied;
    }

    return AtomicUpdateResponse.OutcomeType.NoChanges;
  }

  public IEnumerable<IValidationResult> ValidateProfile(ProfileVo profileVo)
  {
    if (profileVo.Name.Length < 10)
    {
      yield return new ValidationResult(ValidationResultType.Fail, "", "");  
    }

    if (profileVo.Name.Length > 200)
    {
      yield return new ValidationResult(ValidationResultType.Fail, "", "");
    }
  }
}