using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Diplomm.Data;
using Diplomm.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Diplomm.Areas.Account.Models;
using Microsoft.AspNetCore.Authorization;

namespace Diplomm.Controllers
{
    public class EmployeesTablesController : Controller
    {
        private readonly UserManager<EmployeesTable> _userManager;
        private readonly SignInManager<EmployeesTable> _signInManager;
        private readonly AppDbContext _context;

        public EmployeesTablesController(UserManager<EmployeesTable> userManager, AppDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EmployeesTables
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeesTables.ToListAsync());
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
        public async Task<IActionResult> Create(RegisterModel model)
        {
            var user = model.GetUser();
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
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
        public async Task<IActionResult> Edit(int id, [Bind("EmployeesId,Name,Surname,Patronymic,UserName,Password")] EmployeesTable employeesTable)
        {
            if (id != employeesTable.Id)
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
                    if (!EmployeesTableExists(employeesTable.Id))
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesTable = await _context.EmployeesTables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeesTable == null)
            {
                return NotFound();
            }

            return View(employeesTable);
        }
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var employeesTable = await _context.EmployeesTables.FindAsync(id);
        //    if (employeesTable != null)
        //    {
        //        _context.EmployeesTables.Remove(employeesTable);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        // GET: EmployeesTables/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            EmployeesTable user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }


        private bool EmployeesTableExists(int id)
        {
            return _context.EmployeesTables.Any(e => e.Id == id);
        }
    }
}
