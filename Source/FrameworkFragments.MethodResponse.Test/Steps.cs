using NUnit.Framework;

namespace FrameworkFragments.MethodResponse.Test;

[Binding]
public class Steps
{
  [Given(@"implicit cast w/o error")]
  public void GivenImplicitCastWoError()
  {
    MethodResponse<MyEnum> x = MyEnum.Success;
    Assert.IsFalse(x.IsError());
    Assert.AreEqual(MyEnum.Success, x.Outcome);
  }

  enum MyEnum
  {
    Error = 1,
    Success = 2
  }

  [Given(@"implicit cast with error")]
  public void GivenImplicitCastWithError()
  {
    MethodResponse<MyEnum> x = new ErrorInfo.ErrorInfo("abc", "xyz");
    Assert.IsTrue(x.IsError());
    Assert.AreEqual(MyEnum.Error, x.Outcome);
  }
}