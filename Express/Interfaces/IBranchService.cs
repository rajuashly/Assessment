using Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Interfaces
{
    public interface IBranchService
    {
        Task<Branch> GetBranch(int id);
        Task<IEnumerable<Branch>> GetAllBranches();
        Task<int> CreateBranch(Branch branch);
        Task<bool> UpdateBranch(Branch branch);
        Task<bool> DeleteBranch(int id);
    }
}
