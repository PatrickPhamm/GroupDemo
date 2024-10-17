using KoiShowManagementSystem.Data.Base;
using KoiShowManagementSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KoiShowManagementSystem.Data.Repository
{
    public class JudgeRepository : GenericRepository<Judge>
    {
        public JudgeRepository() { }
        public JudgeRepository(FA24_SE1717_PRN231_G2_KoiShowManagementSystemContext context) => _context = context;

        public async Task<List<Judge>> GetAllAsync()
        {
            //return await _context.Results.Include("Judges").ToListAsync();
            return await _context.Judges.ToListAsync();
            //return await _context.Results.Include(t => t.JudgesId).ToListAsync();
        }

        /*public async Task<Image?> GetJudgeByUrl(string url)
        {
            return await _context.Judges
                .Where(i => i.DeletedBy == null)
                .SingleOrDefaultAsync(i => i.Url == url);
        }*/
    }
}
