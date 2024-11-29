namespace ServiceReport.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync();
    }
}
