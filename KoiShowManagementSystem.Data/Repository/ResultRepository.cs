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
    public class ResultRepository : GenericRepository<Result>
    {
        public ResultRepository() { }
        public ResultRepository(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context) => _context = context;

        public async Task<List<Result>> GetAllAsync()
        {
            //return await _context.Results.Include("Judges").ToListAsync();
            return await _context.Results.ToListAsync();
            //return await _context.Results.Include(t => t.JudgesId).ToListAsync();
        }

        /*public async Task<int> UpdateAsync()
        {
            return await _context.Results.;
        }*/
    }
}
