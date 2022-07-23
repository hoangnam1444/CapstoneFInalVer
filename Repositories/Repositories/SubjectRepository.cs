using Contracts.Repositories;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SubjectRepository : RepositoryBase<Subject> ,ISubjectRepository
    {
        public SubjectRepository(DataContext context) : base(context)
        {
        }

        public async Task<string> GetName(List<int> savedPointSubjects)
        {
            var result = "";
            foreach(var id in savedPointSubjects)
            {
                result += await FindByCondition(x => x.Id == id, false).Select(x => x.Name).FirstOrDefaultAsync()+", ";
            }
            return result.Substring(0, result.Length - 2);
        }
    }
}