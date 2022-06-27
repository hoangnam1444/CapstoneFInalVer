using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.HandleServices
{
    public interface IUserAccessor
    {
        int GetAccountId();
        int GetAccountRole();
        void SendEmail(string name, string toEmail, string code);
    }
}
