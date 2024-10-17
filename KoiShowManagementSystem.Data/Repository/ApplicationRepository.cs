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
    public class ApplicationRepository : GenericRepository<Application>
    {
        public ApplicationRepository() { }
        public ApplicationRepository(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context) => _context = context;
        
        public async Task<List<Application>> GetAllAsync()
        {
            return await _context.Applications.ToListAsync();
        }

    }
}
