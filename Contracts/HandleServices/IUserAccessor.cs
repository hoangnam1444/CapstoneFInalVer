using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.HandleServices
{
    public interface IUserAccessor
    {
        int GetAccountId();
        int GetAccountRole();
    }
}
