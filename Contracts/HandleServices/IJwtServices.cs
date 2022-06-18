using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.HandleServices
{
    public interface IJwtServices
    {
        string CreateToken(int role, int accountId);
    }
}
