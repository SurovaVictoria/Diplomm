using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomm.Models.Tables
{
    public class ChangesTable
    {
        [Key]
        public int ChangeId { get; set; }
        public DateTime DateChange { get; set; }
        public bool Cancel {  get; set; }
        public bool Replacement { get; set; }
        public int? fkTimetable { get; set; }
        [ForeignKey("fkTimetable")]
        public TimetableTable? Timetable { get; set; }

        public int? fkEmployee { get; set; }
        [ForeignKey("fkEmployee")]
        public EmployeesTable? Employees { get; set; }
    }
}
