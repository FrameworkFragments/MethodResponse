using System;
using FrameworkFragments.ErrorInfo;

namespace FrameworkFragments.MethodResponse;

public interface IMethodResponse<TOutcomeType> where TOutcomeType : Enum
{
  public TOutcomeType Outcome { get; }

  public bool IsError => null != ErrorInfo;

  public IErrorInfo? ErrorInfo { get; }

  public bool Is(TOutcomeType outcomeType)
  {
    return 0 == Outcome.CompareTo(outcomeType);
  }
}