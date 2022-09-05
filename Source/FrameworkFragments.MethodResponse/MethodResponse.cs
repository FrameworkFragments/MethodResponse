using System;
using FrameworkFragments.ErrorInfo;

namespace FrameworkFragments.MethodResponse;

public class MethodResponse<TOutcomeType> : IMethodResponse<TOutcomeType> where TOutcomeType : struct, Enum
{
  protected MethodResponse(TOutcomeType outcome)
  {
    Outcome = outcome;
  }

  protected MethodResponse(TOutcomeType outcome, IErrorInfo? errorInfo)
    : this(outcome)
  {
    ErrorInfo = errorInfo;
  }

  public TOutcomeType Outcome { get; }
  public IErrorInfo? ErrorInfo { get; }

  public bool Is(TOutcomeType resultType)
  {
    return 0 == resultType.CompareTo(Outcome);
  }

  public bool IsError()
  {
    return null != ErrorInfo;
  }

  public static implicit operator MethodResponse<TOutcomeType>(TOutcomeType outcomeType) => new(outcomeType, null);
  public static implicit operator MethodResponse<TOutcomeType>(ErrorInfo.ErrorInfo errorInfo) => new(Enum.Parse<TOutcomeType>("Error"), errorInfo);
}