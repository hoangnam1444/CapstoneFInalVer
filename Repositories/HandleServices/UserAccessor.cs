using Contracts.HandleServices;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Repositories.HandleServices
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetAccountId()
        {
            return int.Parse(httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type == "AccountId").Value);
        }

        public int GetAccountRole()
        {
            return int.Parse(httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type == "Role").Value);
        }
    }
}
