using Contracts.Repositories;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SecurityCodeRepository : RepositoryBase<SecurityCode>, ISecurityCodeRepository
    {
        public SecurityCodeRepository(DataContext context) : base(context)
        {

        }
        public async Task<bool> ActivatedCode(string code, int userId)
        {
            var result = true;
            var securityCode = await FindByCondition(x => x.Code == code && x.UserId == userId, false).FirstOrDefaultAsync();
            if (securityCode == null) result = false;
            else
            {
                Delete(securityCode);
            }
            return result;
        }

        public async Task<SecurityCode> Create(int userId)
        {
            var result = new SecurityCode();
            var sCode = "";
            var random = new Random();
            var code = random.Next(0000, 9999);
            if (code < 10)
            {
                sCode = "000" + code;
            }
            else if (code < 100)
            {
                sCode = "00" + code;
            }
            else if (code < 1000)
            {
                sCode = "0" + code;
            }
            else
            {
                sCode = code.ToString();
            }
            var oldCode = await FindByCondition(x => x.UserId == userId, true).FirstOrDefaultAsync();
            if (oldCode == null)
            {
                var newCode = new SecurityCode
                {
                    Code = sCode,
                    UserId = userId
                };
                Create(newCode);
                result = newCode;
            }
            else
            {
                oldCode.Code = sCode;
                Update(oldCode);
                result = oldCode;
            }
            return result;
        }
    }
}
