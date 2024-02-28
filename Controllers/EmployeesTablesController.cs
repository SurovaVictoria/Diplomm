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
    public class EmployeesTablesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesTablesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EmployeesTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeesTables.ToListAsync());
        }

        // GET: EmployeesTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesTable = await _context.EmployeesTables
                .FirstOrDefaultAsync(m => m.EmployeesId == id);
            if (employeesTable == null)
            {
                return NotFound();
            }

            return View(employeesTable);
        }

        // GET: EmployeesTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeesTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeesId,Name,Surname,Patronymic,Email")] EmployeesTable employeesTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeesTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeesTable);
        }

        // GET: EmployeesTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesTable = await _context.EmployeesTables.FindAsync(id);
            if (employeesTable == null)
            {
                return NotFound();
            }
            return View(employeesTable);
        }

        // POST: EmployeesTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeesId,Name,Surname,Patronymic,Email")] EmployeesTable employeesTable)
        {
            if (id != employeesTable.EmployeesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeesTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesTableExists(employeesTable.EmployeesId))
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
            return View(employeesTable);
        }

        // GET: EmployeesTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesTable = await _context.EmployeesTables
                .FirstOrDefaultAsync(m => m.EmployeesId == id);
            if (employeesTable == null)
            {
                return NotFound();
            }

            return View(employeesTable);
        }

        // POST: EmployeesTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeesTable = await _context.EmployeesTables.FindAsync(id);
            if (employeesTable != null)
            {
                _context.EmployeesTables.Remove(employeesTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesTableExists(int id)
        {
            return _context.EmployeesTables.Any(e => e.EmployeesId == id);
        }
    }
}
