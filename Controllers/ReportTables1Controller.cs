using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplomm.Data;
using Diplomm.Models.Tables;
using Microsoft.IdentityModel.Tokens;

namespace Diplomm.Controllers
{
    public class ReportTables1Controller : Controller
    {
        private readonly AppDbContext _context;

        public ReportTables1Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: ReportTables1
        public async Task<IActionResult> Index(string type, int employee)
        {
            
            IQueryable<ReportTable> appDbContext = _context.ReportTables
                .Include(r => r.Group)
                .Include(r => r.Lesson)
                .Include(r => r.Replacement);

            var par = Request.Query;
            if (par.Count > 0)
            {
                DateTime dateFrom = DateTime.Parse(par["FromDate"]);
                DateTime dateTo = DateTime.Parse(par["ToDate"]);
                appDbContext = appDbContext.Where(it => it.DateTime <= dateTo && it.DateTime >= dateFrom);
            }
            if (!String.IsNullOrEmpty(type) && !type.Equals("Все"))
            {
                appDbContext = appDbContext.Where(it => it.Type == type);
            }
            if (employee != null && employee != 0)
            {
                appDbContext = appDbContext.Where(it => it.fkReplacement == employee);
            }
            //List<EmployeesTable> emp = _context.EmployeesTables.ToList();
            //emp.Insert(0, new EmployeesTable { Name = "Все", EmployeesId = 0 });

            return View(await appDbContext.ToListAsync());
        }

        // GET: ReportTables1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTable = await _context.ReportTables
                .Include(r => r.Group)
                .Include(r => r.Lesson)
                .Include(r => r.Replacement)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (reportTable == null)
            {
                return NotFound();
            }

            return View(reportTable);
        }

        // GET: ReportTables1/Create
        public IActionResult Create()
        {
            ViewData["fkGroup"] = new SelectList(_context.Groups, "GroupId", "GroupName");
            ViewData["fkLesson"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");
            ViewData["fkReplacement"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName");
            return View();
        }

        // POST: ReportTables1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,DateTime,Employee,Type,fkLesson,fkGroup,fkReplacement")] ReportTable reportTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["fkGroup"] = new SelectList(_context.Groups, "GroupId", "GroupName", reportTable.fkGroup);
            ViewData["fkLesson"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", reportTable.fkLesson);
            ViewData["fkReplacement"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName", reportTable.fkReplacement);
            return View(reportTable);
        }

        // GET: ReportTables1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTable = await _context.ReportTables.FindAsync(id);
            if (reportTable == null)
            {
                return NotFound();
            }
            ViewData["fkGroup"] = new SelectList(_context.Groups, "GroupId", "GroupName", reportTable.fkGroup);
            ViewData["fkLesson"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", reportTable.fkLesson);
            ViewData["fkReplacement"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName", reportTable.fkReplacement);
            return View(reportTable);
        }

        // POST: ReportTables1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,DateTime,Employee,Type,fkLesson,fkGroup,fkReplacement")] ReportTable reportTable)
        {
            if (id != reportTable.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportTableExists(reportTable.ReportId))
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
            ViewData["fkGroup"] = new SelectList(_context.Groups, "GroupId", "GroupName", reportTable.fkGroup);
            ViewData["fkLesson"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", reportTable.fkLesson);
            ViewData["fkReplacement"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName", reportTable.fkReplacement);
            return View(reportTable);
        }

        // GET: ReportTables1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportTable = await _context.ReportTables
                .Include(r => r.Group)
                .Include(r => r.Lesson)
                .Include(r => r.Replacement)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (reportTable == null)
            {
                return NotFound();
            }

            return View(reportTable);
        }

        // POST: ReportTables1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportTable = await _context.ReportTables.FindAsync(id);
            if (reportTable != null)
            {
                _context.ReportTables.Remove(reportTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportTableExists(int id)
        {
            return _context.ReportTables.Any(e => e.ReportId == id);
        }
    }
}
