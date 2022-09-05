using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace FrameworkFragments.MethodResponse;

public class Source
{
  private static readonly ConcurrentDictionary<string, Source> _memo = new();
  private readonly string _source;

  private Source(string source)
  {
    _source = source;
  }

  public static Source CurrentMethod(
    [CallerMemberName] string memberName = "",
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
  )
  {
    var source = string.Concat(sourceFilePath, ":", sourceLineNumber, memberName);
    if (!_memo.ContainsKey(source))
      _memo.AddOrUpdate(source, new Source(source), (_, existingSource) => existingSource);

    return _memo[source];
  }

  public override string ToString()
  {
    return _source;
  }
}