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
    public class ChangesTablesController : Controller
    {
        private readonly AppDbContext _context;

        public ChangesTablesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ChangesTables
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ChangesTables.Include(c => c.Employees).Include(c => c.Timetable);

            IQueryable<ChangesTable> context = _context.ChangesTables
                .Include(r => r.Timetable)
                .Include(r => r.Employees);

            var par = Request.Query;
            if (par.Count > 0)
            {
                DateTime dateFrom = DateTime.Parse(par["FromDate"]);
                DateTime dateTo = DateTime.Parse(par["ToDate"]);
                context = appDbContext.Where(it => it.DateChange <= dateTo && it.DateChange >= dateFrom);
            }
            //if (!String.IsNullOrEmpty(type) && !type.Equals("Все"))
            //{
            //    appDbContext = appDbContext.Where(it => it.Type == type);
            //}
            //if (employee != null && employee != 0)
            //{
            //    appDbContext = appDbContext.Where(it => it.fkReplacement == employee);
            //}
            return View(await appDbContext.ToListAsync());
        }

        // GET: ChangesTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changesTable = await _context.ChangesTables
                .Include(c => c.Employees)
                .Include(c => c.Timetable)
                .FirstOrDefaultAsync(m => m.ChangeId == id);
            if (changesTable == null)
            {
                return NotFound();
            }

            return View(changesTable);
        }

        // GET: ChangesTables/Create
        public IActionResult Create(int? idTimetable, int? idEmployee)
        {
            var cancelItem = _context.ChangesTables.Where(c => c.fkTimetable == idTimetable && c.fkEmployee == idEmployee);

            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "EmployeesId", idEmployee);
            ViewData["fkTimetable"] = new SelectList(_context.TimetableTables, "TimetableID", "TimetableID", idTimetable);
            return View();
        }

        // POST: ChangesTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChangeId,DateChange,Cancel,Replacement,fkTimetable,fkEmployee")] ChangesTable changesTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(changesTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "EmployeesId", changesTable.fkEmployee);
            ViewData["fkTimetable"] = new SelectList(_context.TimetableTables, "TimetableID", "TimetableID", changesTable.fkTimetable);
            return View(changesTable);
        }

        // GET: ChangesTables/Create
        public IActionResult CreateReplacement(int? idTimetable, int? idEmployee)
        {
            var cancelItem = _context.ChangesTables.Where(c => c.fkTimetable == idTimetable && c.fkEmployee == idEmployee);

            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "EmployeesId", idEmployee);
            ViewData["fkTimetable"] = new SelectList(_context.TimetableTables, "TimetableID", "TimetableID", idTimetable);
            return View();
        }

        // POST: ChangesTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReplacement([Bind("ChangeId,DateChange,Cancel,Replacement,fkTimetable,fkEmployee")] ChangesTable changesTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(changesTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "EmployeesId", changesTable.fkEmployee);
            ViewData["fkTimetable"] = new SelectList(_context.TimetableTables, "TimetableID", "TimetableID", changesTable.fkTimetable);
            return View(changesTable);
        }

        // GET: ChangesTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changesTable = await _context.ChangesTables.FindAsync(id);
            if (changesTable == null)
            {
                return NotFound();
            }
            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "EmployeesId", changesTable.fkEmployee);
            ViewData["fkTimetable"] = new SelectList(_context.TimetableTables, "TimetableID", "TimetableID", changesTable.fkTimetable);
            return View(changesTable);
        }

        // POST: ChangesTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChangeId,DateChange,Cancel,Replacement,fkTimetable,fkEmployee")] ChangesTable changesTable)
        {
            if (id != changesTable.ChangeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(changesTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChangesTableExists(changesTable.ChangeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "EmployeesId", changesTable.fkEmployee);
            ViewData["fkTimetable"] = new SelectList(_context.TimetableTables, "TimetableID", "TimetableID", changesTable.fkTimetable);
            return View(changesTable);
        }

        // GET: ChangesTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var changesTable = await _context.ChangesTables
                .Include(c => c.Employees)
                .Include(c => c.Timetable)
                .FirstOrDefaultAsync(m => m.ChangeId == id);
            if (changesTable == null)
            {
                return NotFound();
            }

            return View(changesTable);
        }

        // POST: ChangesTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var changesTable = await _context.ChangesTables.FindAsync(id);
            if (changesTable != null)
            {
                _context.ChangesTables.Remove(changesTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChangesTableExists(int id)
        {
            return _context.ChangesTables.Any(e => e.ChangeId == id);
        }
    }
}
