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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Diplomm.Controllers
{
    public class TimetableTablesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<EmployeesTable> _userManager;

        public TimetableTablesController(UserManager<EmployeesTable> userManager, AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TimetableTables
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<TimetableTable> timeTableByGroup = await _context.TimetableTables
                .Include(t => t.Employee)
                .Include(t => t.Organization)
                .Include(t => t.Post)
                .ToListAsync();

            var appDbContext = _context.TimetableTables.Include(t => t.Employee).Include(t => t.Organization).Include(t => t.Post);
            return View(timeTableByGroup.GroupBy(s => s.Organization).ToList());
        }

        // GET: TimetableTables/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create(DayOfWeeks? dayOfWeek, int? idShop)
        {
            var currentDayItems = _context.TimetableTables.Where(t => t.DayOfWeek == dayOfWeek && t.fkOrganizations == idShop);
            int currentMaxNumber = 0;
            if (currentDayItems.Count() > 0)
            {
                currentMaxNumber = currentDayItems.Max(m => m.Number);
            }
            ViewBag.NextNum = currentMaxNumber + 1;
            ViewData["fkEmployees"] = new SelectList(_context.EmployeesTables, "Id", "FullName");
            ViewData["fkOrganizations"] = new SelectList(_context.OrganizationTables, "ShopId", "ShopName", idShop);
            ViewData["fkPosts"] = new SelectList(_context.Posts, "PostId", "PostName");
            return View();
        }


        // POST: TimetableTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("TimetableID,DayOfWeek,Number,fkPosts,fkOrganizations,fkEmployees")] TimetableTable timetableTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timetableTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["fkEmployees"] = new SelectList(_context.EmployeesTables, "Id", "FullName", timetableTable.fkEmployees);
            ViewData["fkOrganizations"] = new SelectList(_context.OrganizationTables, "ShopId", "ShopName", timetableTable.fkOrganizations);
            ViewData["fkPosts"] = new SelectList(_context.Posts, "PostId", "PostName", timetableTable.fkPosts);
            return View(timetableTable);
        }

        // GET: TimetableTables/Edit/5
        [Authorize(Roles = "Admin")]
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
            ViewData["fkEmployees"] = new SelectList(_context.EmployeesTables, "Id", "FullName", timetableTable.fkEmployees);
            ViewData["fkOrganizations"] = new SelectList(_context.OrganizationTables, "ShopId", "ShopName", timetableTable.fkOrganizations);
            ViewData["fkPosts"] = new SelectList(_context.Posts, "PostId", "PostName", timetableTable.fkPosts);
            return View(timetableTable);
        }

        // POST: TimetableTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("TimetableID,DayOfWeek,Number,fkPosts,fkOrganizations,fkEmployees")] TimetableTable timetableTable)
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
            ViewData["fkEmployees"] = new SelectList(_context.EmployeesTables, "Id", "FullName", timetableTable.fkEmployees);
            ViewData["fkOrganizations"] = new SelectList(_context.OrganizationTables, "ShopId", "ShopName", timetableTable.fkOrganizations);
            ViewData["fkPosts"] = new SelectList(_context.Posts, "PostId", "PostName", timetableTable.fkPosts);
            return View(timetableTable);
        }

        // GET: TimetableTables/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timetableTable = await _context.TimetableTables
                .Include(t => t.Employee)
                .Include(t => t.Organization)
                .Include(t => t.Post)
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
        [Authorize(Roles = "Admin")]
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
