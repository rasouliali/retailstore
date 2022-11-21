using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetailStorePrj.Data;

namespace RetailStorePrj.Controllers
{
    public class WorkstationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkstationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Workstations
        [Route("/Workstations/Index/{RetailStoreId}")]
        public async Task<IActionResult> Index(Guid RetailStoreId)
        {
            ViewBag.RetailStoreId = RetailStoreId;
            var RetailStore = await _context.RetailStores.FindAsync(RetailStoreId);
            ViewBag.RetailStoreName = RetailStore.Name;

            var data = _context.Workstations.AsNoTracking().Where(r => r.RetailStoreId == RetailStoreId);
            return View(await data
                .ToListAsync());
        }


        // GET: Workstations/Details/5
        public async Task<IActionResult> Details(int? id, Guid RetailStoreId)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.RetailStoreId = RetailStoreId;

            var workstation = await _context.Workstations
                .FirstOrDefaultAsync(m => m.Id == id && m.RetailStoreId== RetailStoreId);
            if (workstation == null)
            {
                return NotFound();
            }

            return View(workstation);
        }

        // GET: Workstations/Create
        public IActionResult Create(Guid RetailStoreId)
        {
            ViewBag.RetailStoreId = RetailStoreId;
            return View();
        }

        // POST: Workstations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,IsActive")] Workstation workstation, Guid RetailStoreId)
        {
            if (ModelState.IsValid)
            {
                ViewBag.RetailStoreId = RetailStoreId;
                workstation.RetailStoreId = RetailStoreId;
                _context.Add(workstation);
                await _context.SaveChangesAsync();
                return Redirect("/Workstations/Index/"+ RetailStoreId);
            }
            return View(workstation);
        }

        // GET: Workstations/Edit/5
        public async Task<IActionResult> Edit(int? id,Guid? RetailStoreId)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.RetailStoreId = RetailStoreId;

            var workstation = await _context.Workstations.FindAsync(id); 

            if (workstation == null || workstation.RetailStoreId != RetailStoreId)
            {
                return NotFound();
            }
            return View(workstation);
        }

        // POST: Workstations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,IsActive")] Workstation workstation, Guid RetailStoreId)
        {
            if (id != workstation.Id)
            {
                return NotFound();
            }
            ViewBag.RetailStoreId = RetailStoreId;

            if (ModelState.IsValid)
            {
                try
                {
                    workstation.RetailStoreId = RetailStoreId;
                    _context.Update(workstation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkstationExists(workstation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Workstations/Index/"+ RetailStoreId);
            }
            return View(workstation);
        }

        // GET: Workstations/Delete/5
        public async Task<IActionResult> Delete(int? id, Guid RetailStoreId)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.RetailStoreId = RetailStoreId;

            var workstation = await _context.Workstations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workstation == null || workstation.RetailStoreId != RetailStoreId)
            {
                return NotFound();
            }

            return View(workstation);
        }

        // POST: Workstations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, Guid RetailStoreId)
        {
            ViewBag.RetailStoreId = RetailStoreId;
            var workstation = await _context.Workstations.FindAsync(id);
            if (workstation == null || workstation.RetailStoreId != RetailStoreId)
            {
                return NotFound();
            }
            _context.Workstations.Remove(workstation);
            await _context.SaveChangesAsync();
            return Redirect("/Workstations/Index/"+ RetailStoreId);
        }

        private bool WorkstationExists(int id)
        {
            return _context.Workstations.Any(e => e.Id == id);
        }
    }
}
