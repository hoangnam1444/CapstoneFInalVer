namespace Contracts.HandleServices
{
    public interface IJwtServices
    {
        string CreateToken(int role, int accountId);
    }
}
