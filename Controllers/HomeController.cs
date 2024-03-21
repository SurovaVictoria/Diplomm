using Diplomm.Data;
using Diplomm.Models;
using Diplomm.Models.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Diplomm.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Получаем текущее рассписние
            List<TimetableTable> timeTableByGroup = await _context.TimetableTables
                .Include(t => t.Employee)
                .Include(t => t.Group)
                .Include(t => t.Subject)
                .ToListAsync();

            // Получить период дат текущей недели

            var deltaDaysOfWeekToMonday = DayOfWeek.Monday - DateTime.Now.DayOfWeek;
            DateTime dateMonday = DateTime.Now.AddDays(deltaDaysOfWeekToMonday);
            DateTime dateSaturday = dateMonday.AddDays(5);
            // Получаем изменения текущей недели
            //var changeTimetable = _context.ChangesTables.Include();

            // Привести текущее рассписание в актуальный вид
            // foreach(var change in changeTimetable) { }
            //int idTimeTable = 0;
            //var changeItem = timeTableByGroup.Where(it => it.TimetableID == idTimeTable).FirstOrDefault();
            //if(changeItem != null)
            //{
            //    changeItem.fkEmployees = 0;
            //    //changeItem.Description = "";
            //}

            //
            return View(timeTableByGroup.GroupBy(s => s.Group).ToList());
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
