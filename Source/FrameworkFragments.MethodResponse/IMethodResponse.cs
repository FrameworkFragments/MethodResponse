using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkFragments.MethodResponse;

public interface IMethodResponse<TMethodOutcomeType>: IEquatable<TMethodOutcomeType>
  where TMethodOutcomeType : struct, Enum, IComparable
{
  TMethodOutcomeType Outcome { get; }
  public IEnumerable<string> MessageCodes { get; }

  public bool Is(TMethodOutcomeType other)
  {
    return 0 == Outcome.CompareTo(other);
  }
  
  public bool IsNot(TMethodOutcomeType other)
  {
    return 0 == Outcome.CompareTo(other);
  }

  public bool IsIn(IEnumerable<TMethodOutcomeType> outcomeTypes)
  {
    return outcomeTypes.Any(Is);
  }
}