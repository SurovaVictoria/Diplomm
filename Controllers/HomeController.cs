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

        private string[] Months = new string[] { "Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря" };
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Получаем текущее рассписние
            List<TimetableTable> timeTableByGroup = await _context.TimetableTables
                .Include(t => t.Employee)
                .Include(t => t.Organization)
                .Include(t => t.Post)
                .ToListAsync();

            // Получить период дат текущей недели

            var deltaDaysOfWeekToMonday = DayOfWeek.Monday - DateTime.Now.DayOfWeek;
            DateTime dateMonday = DateTime.Now.AddDays(deltaDaysOfWeekToMonday);
            DateTime dateSaturday = dateMonday.AddDays(5);

            ViewBag.Dates = $"{dateMonday.Day} {Months[dateMonday.Month - 1]} - {dateSaturday.Day} {Months[dateSaturday.Month - 1]}";
            // Получаем изменения текущей недели
            var changeTimetable = _context.ChangesTables.Where(it => it.DateChange >= dateMonday.Date && it.DateChange <= dateSaturday.Date)
                .Include(t => t.Employees)
                .Include(t => t.Posts)
                .ToList();

            // Привели текущее рассписание в актуальный вид
            foreach(var change in changeTimetable)
            {
                var editItem = timeTableByGroup.Where(it => it.TimetableID == change.fkTimetable).FirstOrDefault();
                if(change.Replacement)
                {
                    editItem.Employee = change.Employees;
                    editItem.Post = change.Posts;
                }
                else
                {
                    editItem.Number = 0;
                }
            }
            
            return View(timeTableByGroup.GroupBy(s => s.Organization).ToList());
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
