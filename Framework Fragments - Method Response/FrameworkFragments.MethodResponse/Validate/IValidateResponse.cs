namespace Smelter.Common.MethodResponse
{
    public interface IValidateResponse: IMethodResponse
    {
        public bool IsValid();
    }
}