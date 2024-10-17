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
    public class KoiRepository : GenericRepository<Koi>
    {
        public KoiRepository() { }
        public KoiRepository(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context) => _context = context;

        public async Task<List<Koi>> GetAllAsync()
        {
            //return await _context.Kois.Include(s => s.Applications).Include(s => s.Results).ToListAsync();
            return await _context.Kois.ToListAsync();
        }
    }
}
