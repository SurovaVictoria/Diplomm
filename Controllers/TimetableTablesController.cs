using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplomm.Data;
using Diplomm.Models.Tables;
using Diplomm.Models;

namespace Diplomm.Controllers
{
    public class TimetableTablesController : Controller
    {
        private readonly AppDbContext _context;

        public TimetableTablesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TimetableTables
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TimetableTables.Include(t => t.Employee).Include(t => t.Group).Include(t => t.Subject);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TimetableTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetableTable = await _context.TimetableTables
                .Include(t => t.Employee)
                .Include(t => t.Group)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.TimetableID == id);
            if (timetableTable == null)
            {
                return NotFound();
            }

            return View(timetableTable);
        }

        // GET: TimetableTables/Create
        public IActionResult Create(DayOfWeeks? dayOfWeek, int? idGroup)
        {
            var currentDayItems = _context.TimetableTables.Where(t => t.DayOfWeek == dayOfWeek && t.fkGroups == idGroup);
            int currentMaxNumber = 0;
            if (currentDayItems.Count() > 0)
            {
                currentMaxNumber = currentDayItems.Max(m => m.Number);
            }
            ViewBag.NextNum = currentMaxNumber + 1;
            ViewData["fkEmployees"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName");
            ViewData["fkGroups"] = new SelectList(_context.Groups, "GroupId", "GroupName", idGroup);
            ViewData["fkSubjects"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            return View();
        }


        // POST: TimetableTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimetableID,DayOfWeek,Number,fkSubjects,fkGroups,fkEmployees")] TimetableTable timetableTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timetableTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["fkEmployees"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName", timetableTable.fkEmployees);
            ViewData["fkGroups"] = new SelectList(_context.Groups, "GroupId", "GroupName", timetableTable.fkGroups);
            ViewData["fkSubjects"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", timetableTable.fkSubjects);
            return View(timetableTable);
        }

        // GET: TimetableTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetableTable = await _context.TimetableTables.FindAsync(id);
            if (timetableTable == null)
            {
                return NotFound();
            }
            ViewData["fkEmployees"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName", timetableTable.fkEmployees);
            ViewData["fkGroups"] = new SelectList(_context.Groups, "GroupId", "GroupName", timetableTable.fkGroups);
            ViewData["fkSubjects"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", timetableTable.fkSubjects);
            return View(timetableTable);
        }

        // POST: TimetableTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimetableID,DayOfWeek,Number,fkSubjects,fkGroups,fkEmployees")] TimetableTable timetableTable)
        {
            if (id != timetableTable.TimetableID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timetableTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimetableTableExists(timetableTable.TimetableID))
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
            ViewData["fkEmployees"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName", timetableTable.fkEmployees);
            ViewData["fkGroups"] = new SelectList(_context.Groups, "GroupId", "GroupName", timetableTable.fkGroups);
            ViewData["fkSubjects"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", timetableTable.fkSubjects);
            return View(timetableTable);
        }

        // GET: TimetableTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetableTable = await _context.TimetableTables
                .Include(t => t.Employee)
                .Include(t => t.Group)
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(m => m.TimetableID == id);
            if (timetableTable == null)
            {
                return NotFound();
            }

            return View(timetableTable);
        }

        // POST: TimetableTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timetableTable = await _context.TimetableTables.FindAsync(id);
            if (timetableTable != null)
            {
                _context.TimetableTables.Remove(timetableTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimetableTableExists(int id)
        {
            return _context.TimetableTables.Any(e => e.TimetableID == id);
        }
    }
}
