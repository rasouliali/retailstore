using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailStorePrj.Data;

namespace RetailStorePrj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailStoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RetailStoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RetailStores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RetailStore>>> GetRetailStores()
        {
            var list = await _context.RetailStores.Include(e => e.Childs).Where(r => r.ParentId == null).ToListAsync();
            await loadList(list);
            return list;
        }

        private async Task loadList(List<RetailStore> list)
        {
            foreach (var item in list)
            {
                item.Childs = await _context.RetailStores.Include(r => r.Childs).Where(r => r.ParentId == item.Id).ToListAsync();
            }
        }

        // GET: api/RetailStores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RetailStore>> GetRetailStore(Guid id)
        {
            try
            {

                var RetailStore = await _context.RetailStores.FromSqlRaw($"exec sp_getRetailStoreById @id", new Microsoft.Data.SqlClient.SqlParameter("id", id)).ToListAsync();

                if (RetailStore == null || RetailStore.Count==0)
                {
                    return NotFound();
                }

                return RetailStore[0];
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        // PUT: api/RetailStores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRetailStore(Guid id, RetailStore RetailStore)
        {
            if (id != RetailStore.Id)
            {
                return BadRequest();
            }

            _context.Entry(RetailStore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RetailStoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RetailStores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RetailStore>> PostRetailStore([FromBody]RetailStore RetailStoredata)
        {
            RetailStoredata.IpAddress = Request.HttpContext.GetRemoteIPAddress().ToString();

            _context.RetailStores.Add(RetailStoredata);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRetailStore", new { id = RetailStoredata.Id }, RetailStoredata);
        }

        // DELETE: api/RetailStores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRetailStore(Guid id)
        {
            var RetailStore = await _context.RetailStores.FindAsync(id);
            if (RetailStore == null)
            {
                return NotFound();
            }

            _context.RetailStores.Remove(RetailStore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RetailStoreExists(Guid id)
        {
            return _context.RetailStores.Any(e => e.Id == id);
        }
    }
}
