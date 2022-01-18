using Express.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Interfaces
{
    public interface IBranchRepository
    {
        Task<Branch> GetBranch(int id);
        Task<bool> DeleteBranch(int id);
        Task<IEnumerable<Branch>> GetAllBranches();
        bool BranchExists(int id);
        Task<bool> UpdateBranch(int id, Branch branch);
        Task<int> CreateBranch(Branch branch);
    }
}
