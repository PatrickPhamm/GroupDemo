using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiShowManagementSystem.Data.Models;
using KoiShowManagementSystem.Service;
using KoiShowManagementSystem.Service.Base;

namespace KoiShowManagementSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoisController : ControllerBase
    {

        private readonly IKoiBusiness _service;
        public KoisController(IKoiBusiness service) => _service = service;
        // GET: api/Kois
        [HttpGet]
        public async Task<IBusinessResult> GetKois()
        {
            return await _service.GetAll();
        }

        // GET: api/Kois/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetKoi(int id)
        {
            return await _service.GetById(id);
        }

        // PUT: api/Kois/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IBusinessResult> PutKoi(Koi koi)
        {
            //return await _service.Save(koi);
            return await _service.Update(koi);
        }

        // POST: api/Kois
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostKoi(Koi koi)
        {
            return await _service.Save(koi);
        }

        // DELETE: api/Kois/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteKoi(int id)
        {
            return await _service.DeleteById(id);
        }

        //private bool KoiExists(int id)
        //{
        //    return _context.Kois.Any(e => e.KoiId == id);
        //}
    }
}
