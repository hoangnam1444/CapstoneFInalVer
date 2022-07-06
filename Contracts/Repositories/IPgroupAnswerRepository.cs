using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPgroupAnswerRepository
    {
        Task<List<PerGroup>> GetPGroupResult(List<TestResults> testResult);
    }
}
