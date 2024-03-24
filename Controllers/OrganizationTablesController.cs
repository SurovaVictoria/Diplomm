using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplomm.Data;
using Diplomm.Models.Tables;

namespace Diplomm.Controllers
{
    public class OrganizationTablesController : Controller
    {
        private readonly AppDbContext _context;

        public OrganizationTablesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrganizationTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrganizationTables.ToListAsync());
        }

        // GET: OrganizationTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationTable = await _context.OrganizationTables
                .FirstOrDefaultAsync(m => m.ShopId == id);
            if (organizationTable == null)
            {
                return NotFound();
            }

            return View(organizationTable);
        }

        // GET: OrganizationTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrganizationTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShopId,ShopName,Address")] OrganizationTable organizationTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizationTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizationTable);
        }

        // GET: OrganizationTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationTable = await _context.OrganizationTables.FindAsync(id);
            if (organizationTable == null)
            {
                return NotFound();
            }
            return View(organizationTable);
        }

        // POST: OrganizationTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShopId,ShopName,Address")] OrganizationTable organizationTable)
        {
            if (id != organizationTable.ShopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizationTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationTableExists(organizationTable.ShopId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(organizationTable);
        }

        // GET: OrganizationTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationTable = await _context.OrganizationTables
                .FirstOrDefaultAsync(m => m.ShopId == id);
            if (organizationTable == null)
            {
                return NotFound();
            }

            return View(organizationTable);
        }

        // POST: OrganizationTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizationTable = await _context.OrganizationTables.FindAsync(id);
            if (organizationTable != null)
            {
                _context.OrganizationTables.Remove(organizationTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizationTableExists(int id)
        {
            return _context.OrganizationTables.Any(e => e.ShopId == id);
        }
    }
}
