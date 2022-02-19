namespace FrameworkFragments.MethodResponse.Store
{
    public interface IStoreResponse: IMethodResponse
    {
        public bool IsSaved();
    }
}