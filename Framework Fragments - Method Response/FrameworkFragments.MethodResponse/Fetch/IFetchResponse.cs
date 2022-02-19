namespace Smelter.Common.MethodResponse
{
    public interface IFetchResponse : IMethodResponse
    {
        public bool IsFound();
        public bool IsNotFound();
    }
}