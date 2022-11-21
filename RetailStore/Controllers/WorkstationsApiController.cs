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
    public class WorkstationsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkstationsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/WorkstationsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workstation>>> GetWorkstations()
        {
            return await _context.Workstations.ToListAsync();
        }

        // GET: api/WorkstationsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workstation>> GetWorkstation(int id)
        {
            var workstation = await _context.Workstations.FindAsync(id);

            if (workstation == null)
            {
                return NotFound();
            }

            return workstation;
        }

        // PUT: api/WorkstationsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkstation(int id, Workstation workstation)
        {
            if (id != workstation.Id)
            {
                return BadRequest();
            }

            _context.Entry(workstation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkstationExists(id))
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

        // POST: api/WorkstationsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Workstation>> PostWorkstation(Workstation workstation)
        {
            _context.Workstations.Add(workstation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkstation", new { id = workstation.Id }, workstation);
        }

        // DELETE: api/WorkstationsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkstation(int id)
        {
            var workstation = await _context.Workstations.FindAsync(id);
            if (workstation == null)
            {
                return NotFound();
            }

            _context.Workstations.Remove(workstation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkstationExists(int id)
        {
            return _context.Workstations.Any(e => e.Id == id);
        }
    }
}
