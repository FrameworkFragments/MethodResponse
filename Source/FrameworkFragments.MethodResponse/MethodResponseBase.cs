using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkFragments.MethodResponse;

public record MethodResponseBase<TMethodOutcomeType> : IMethodResponse<TMethodOutcomeType>
  where TMethodOutcomeType : struct, Enum
{
  public TMethodOutcomeType Outcome { get; set; }
  public IEnumerable<string> MessageCodes { get; set; } = Array.Empty<string>();

  public virtual bool Equals(TMethodOutcomeType other)
  {
    return this == other;
  }

  #region Cast Operators
  public static implicit operator TMethodOutcomeType(MethodResponseBase<TMethodOutcomeType> source)
  {
    return source.Outcome;
  }
  
  public static implicit operator MethodResponseBase<TMethodOutcomeType>(Tuple<TMethodOutcomeType, IEnumerable<string>> source)
  {
    var messageCodes = source.Item2.Any() ? source.Item2 : Array.Empty<string>();
    return new MethodResponseBase<TMethodOutcomeType>(){ Outcome = source.Item1, MessageCodes = messageCodes};
  }
  public static implicit operator MethodResponseBase<TMethodOutcomeType>(Tuple<TMethodOutcomeType, string> source)
  {
    var messageCodes = String.IsNullOrWhiteSpace(source.Item2) ? Array.Empty<string>() : new string[] {source.Item2};
    return new MethodResponseBase<TMethodOutcomeType>(){ Outcome = source.Item1, MessageCodes = messageCodes};
  }
  
  public static implicit operator MethodResponseBase<TMethodOutcomeType>(TMethodOutcomeType source)
  {
    return new MethodResponseBase<TMethodOutcomeType>() {Outcome = source};
  }
  #endregion
  
  #region Comparison Operators
  public static bool operator ==(MethodResponseBase<TMethodOutcomeType> left, TMethodOutcomeType right)
  {
    return left.Outcome.Equals(right);
  }

  public static bool operator !=(MethodResponseBase<TMethodOutcomeType> left, TMethodOutcomeType right)
  {
    return !(left == right);
  }

  public static bool operator >(MethodResponseBase<TMethodOutcomeType> left, TMethodOutcomeType right)
  {
    return left.Outcome.CompareTo(right) > 0;
  }

  public static bool operator <(MethodResponseBase<TMethodOutcomeType> left, TMethodOutcomeType right)
  {
    return left.Outcome.CompareTo(right) < 0;
  }

  public static bool operator >=(MethodResponseBase<TMethodOutcomeType> left, TMethodOutcomeType right)
  {
    return left.Outcome.CompareTo(right) >= 0;
  }

  public static bool operator <=(MethodResponseBase<TMethodOutcomeType> left, TMethodOutcomeType right)
  {
    return left.Outcome.CompareTo(right) <= 0;
  }
  #endregion
}