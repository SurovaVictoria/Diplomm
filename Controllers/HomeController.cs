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

        private string[] Months = new string[] { "������", "�������", "�����", "������", "���", "����", "����", "�������", "��������", "�������", "������", "�������" };
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // �������� ������� ����������
            List<TimetableTable> timeTableByGroup = await _context.TimetableTables
                .Include(t => t.Employee)
                .Include(t => t.Group)
                .Include(t => t.Subject)
                .ToListAsync();

            // �������� ������ ��� ������� ������

            var deltaDaysOfWeekToMonday = DayOfWeek.Monday - DateTime.Now.DayOfWeek;
            DateTime dateMonday = DateTime.Now.AddDays(deltaDaysOfWeekToMonday);
            DateTime dateSaturday = dateMonday.AddDays(5);

            ViewBag.Dates = $"{dateMonday.Day} {Months[dateMonday.Month - 1]} - {dateSaturday.Day} {Months[dateSaturday.Month - 1]}";
            // �������� ��������� ������� ������
            var changeTimetable = _context.ChangesTables.Where(it => it.DateChange >= dateMonday.Date && it.DateChange <= dateSaturday.Date)
                .Include(t => t.Employees)
                .Include(t => t.Subjects)
                .ToList();

            // �������� ������� ����������� � ���������� ���
            foreach(var change in changeTimetable)
            {
                var editItem = timeTableByGroup.Where(it => it.TimetableID == change.fkTimetable).FirstOrDefault();
                if(change.Replacement)
                {
                    editItem.Employee = change.Employees;
                    editItem.Subject = change.Subjects;
                }
                else
                {
                    editItem.Number = 0;
                }
            }
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
