namespace FrameworkFragments.MethodResponse.Fetch
{
    public interface IFetchResponse : IMethodResponse
    {
        public bool IsFound();
        public bool IsNotFound();
    }
}