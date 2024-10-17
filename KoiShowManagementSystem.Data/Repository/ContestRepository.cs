using KoiShowManagementSystem.Data.Base;
using KoiShowManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShowManagementSystem.Data.Repository
{
    public class ContestRepository : GenericRepository<Contest>
    {
        public ContestRepository() { }
        public ContestRepository(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context) => _context = context;
        public async Task<List<Contest>> GetAllAsync()
        {
            return await _context.Contests.ToListAsync();
        }
    }
}
