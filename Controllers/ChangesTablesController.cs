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
    public class ChangesTablesController : Controller
    {
        private readonly AppDbContext _context;

        public ChangesTablesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ChangesTables
        public async Task<IActionResult> Index(int? employee)
        {
            IQueryable<TimetableTable> appDbContextTimetable = _context.TimetableTables.Include(r => r.Employee);
            IQueryable<ChangesTable> appDbContext = _context.ChangesTables
                .Include(r => r.Timetable)
                .Include(r => r.Employees);
            IQueryable<ChangesTable> appDbContext2 = _context.ChangesTables
                .Include(r => r.Timetable)
                .Include(r => r.Employees);
            IQueryable<ChangesTable> appDbContext3 = _context.ChangesTables
                .Include(r => r.Timetable)
                .Include(r => r.Employees);
            var par = Request.Query;
            if (par.Count > 0 && employee != null && employee != 0)
            {
                DateTime dateFrom = DateTime.Parse(par["FromDate"]);
                DateTime dateTo = DateTime.Parse(par["ToDate"]);
                appDbContext = appDbContext.Where(it => it.DateChange <= dateTo && it.DateChange >= dateFrom);

                TimeSpan ts = dateTo - dateFrom;
                int count = (int)Math.Floor(ts.TotalDays / 7);
                int remainder = (int)(ts.TotalDays % 7);
                int sinceLastDay = (int)(dateTo.DayOfWeek - DayOfWeek.Monday);
                if (sinceLastDay < 0) sinceLastDay += 7;
                if (remainder >= sinceLastDay) count++;

                int countSt = (int)Math.Floor(ts.TotalDays / 7);
                int remainderSt = (int)(ts.TotalDays % 7);
                int sinceLastDaySt = (int)(dateTo.DayOfWeek - DayOfWeek.Saturday);
                if (sinceLastDaySt < 0) sinceLastDaySt += 7;
                if (remainderSt >= sinceLastDaySt) countSt++;

                int countTu = (int)Math.Floor(ts.TotalDays / 7);
                int remainderTu = (int)(ts.TotalDays % 7);
                int sinceLastDayTu = (int)(dateTo.DayOfWeek - DayOfWeek.Tuesday);
                if (sinceLastDayTu < 0) sinceLastDayTu += 7;
                if (remainderTu >= sinceLastDayTu) countTu++;

                int countTh = (int)Math.Floor(ts.TotalDays / 7);
                int remainderTh = (int)(ts.TotalDays % 7);
                int sinceLastDayTh = (int)(dateTo.DayOfWeek - DayOfWeek.Thursday);
                if (sinceLastDayTh < 0) sinceLastDayTh += 7;
                if (remainderTh >= sinceLastDayTh) countTh++;

                int countWd = (int)Math.Floor(ts.TotalDays / 7);
                int remainderWd = (int)(ts.TotalDays % 7);
                int sinceLastDayWd = (int)(dateTo.DayOfWeek - DayOfWeek.Wednesday);
                if (sinceLastDayWd < 0) sinceLastDayWd += 7;
                if (remainderWd >= sinceLastDayWd) countWd++;

                int countFr = (int)Math.Floor(ts.TotalDays / 7);
                int remainderFr = (int)(ts.TotalDays % 7);
                int sinceLastDayFr = (int)(dateTo.DayOfWeek - DayOfWeek.Friday);
                if (sinceLastDayFr < 0) sinceLastDayFr += 7;
                if (remainderFr >= sinceLastDayFr) countFr++;

                int countSn = (int)Math.Floor(ts.TotalDays / 7);
                int remainderSn = (int)(ts.TotalDays % 7);
                int sinceLastDaySn = (int)(dateTo.DayOfWeek - DayOfWeek.Sunday);
                if (sinceLastDaySn < 0) sinceLastDaySn += 7;
                if (remainderSn >= sinceLastDaySn) countSn++;

                appDbContextTimetable = appDbContextTimetable.Where(it => it.fkEmployees == employee);
                appDbContext = appDbContext.Where(it => it.Replacement == true && it.fkEmployee == employee);
                appDbContext2 = appDbContext2.Where(it => it.Cancel == true && it.Timetable.fkEmployees == employee);
                appDbContext3 = appDbContext3.Where(it => it.Replacement == true && it.Timetable.fkEmployees == employee);

                var fr = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Friday);
                //var sn = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Sunday);
                var th = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Thursday);
                var tu = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Tuesday);
                var st = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Saturday);
                var mn = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Monday);
                var wd = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Wednesday);

                int allTarif = (fr.Count() * countFr) + (th.Count() * countTh) + (tu.Count() * countTu) + (st.Count() * countSt) + (mn.Count() * count) + (wd.Count() * countWd);
                //int countTarif = appDbContextTimetable.Count();
                int countReplacement = appDbContext.Count();
                int countCancel = appDbContext2.Count();
                int countReplacementTwo = appDbContext3.Count();
                int countAll = allTarif + countReplacement - countCancel - countReplacementTwo;

                ViewBag.Tarif = allTarif;
                ViewBag.Replacement = countReplacement;
                ViewBag.Cancel = countCancel;
                ViewBag.Rep = countReplacementTwo;
                ViewBag.All = countAll;

            }

            //if (employee != null && employee != 0)
            //{
            //    appDbContextTimetable = appDbContextTimetable.Where(it => it.fkEmployees == employee);
            //    appDbContext = appDbContext.Where(it => it.Replacement == true  && it.fkEmployee==employee);
            //    appDbContext2 = appDbContext2.Where(it => it.Cancel == true && it.Timetable.fkEmployees == employee);
            //    appDbContext3 = appDbContext3.Where(it => it.Replacement == true && it.Timetable.fkEmployees == employee);

            //    var fr = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Friday);
            //    var sn = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Sunday);
            //    var th = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Thursday);
            //    var tu = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Tuesday);
            //    var st = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Saturday);
            //    var mn = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Monday);
            //    var wd = appDbContextTimetable.Where(it => it.fkEmployees == employee && it.DayOfWeek == DayOfWeeks.Wednesday);

            //    int s = fr.Count() * count

            //    int allTarif = fr.Count() + sn.Count() + th.Count() + tu.Count() + st.Count() + mn.Count() + wd.Count();

            //    int countTarif = appDbContextTimetable.Count();
            //    int countReplacement = appDbContext.Count();
            //    int countCancel = appDbContext2.Count();
            //    int countReplacementTwo = appDbContext3.Count();
            //    int countAll = countTarif + countReplacement - countCancel - countReplacementTwo;

            //    int c = g.Count();
            //    ViewBag.Tarif = countTarif;
            //    ViewBag.Replacement = countReplacement;
            //    ViewBag.Cancel = countCancel;
            //    ViewBag.Rep = countReplacementTwo;
            //    ViewBag.All = countAll;
            //}
            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName");

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
        public IActionResult Create(int idTimetable, ChancheActions chancheAction = ChancheActions.Replace)
        {
            var timetables = _context.TimetableTables
                .Where(it => it.TimetableID == idTimetable)
                .Include(s => s.Post).FirstOrDefault();
            ViewBag.Timetable = timetables;

            // Для простоты ввода данных по умолчанию выбирается дата текущей недели
            // Вычисляем разницу в днях между сегодня и выбранной датой
            var deltaDaysOfWeekToSelect = ((int)timetables.DayOfWeek) - ((int)DateTime.Now.DayOfWeek);
            // Формируем дату выбранного дня недели
            DateTime dateSelect = DateTime.Now.AddDays(deltaDaysOfWeekToSelect);
            ViewBag.DateSelect = dateSelect.ToString("yyyy-MM-dd");

            ViewBag.chancheAction = chancheAction;

            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName");
            ViewData["fkPost"] = new SelectList(_context.Posts, "PostId", "PostName");
            return View();
        }

        // POST: ChangesTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChangeId,DateChange,Cancel,Replacement,fkTimetable,fkEmployee,fkPost")] ChangesTable changesTable, ChancheActions chancheAction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(changesTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            var timetables = _context.TimetableTables
                .Where(it => it.TimetableID == changesTable.fkTimetable)
                .Include(s => s.Post).FirstOrDefault();
            ViewBag.Timetable = timetables;
            var deltaDaysOfWeekToSelect = ((int)timetables.DayOfWeek) - ((int)DateTime.Now.DayOfWeek);
            DateTime dateSelect = DateTime.Now.AddDays(deltaDaysOfWeekToSelect);
            ViewBag.DateSelect = dateSelect.ToString("yyyy-MM-dd");
            ViewBag.chancheAction = chancheAction;
            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName", changesTable.fkEmployee);
            ViewData["fkPost"] = new SelectList(_context.Posts, "PostId", "PostName");
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
            ViewData["fkEmployee"] = new SelectList(_context.EmployeesTables, "EmployeesId", "FullName", changesTable.fkEmployee);
            ViewData["fkTimetable"] = new SelectList(_context.TimetableTables.Include(s => s.Post), "TimetableID", "GetName", changesTable.fkTimetable);
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
