namespace Contracts.HandleServices
{
    public interface IUserAccessor
    {
        int GetAccountId();
        int GetAccountRole();
        void SendEmail(string name, string toEmail, string code);
    }
}
