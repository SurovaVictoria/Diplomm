using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomm.Models.Tables
{
    public class ChangesTable
    {
        [Key]
        public int ChangeId { get; set; }
        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime DateChange { get; set; }
        /// <summary>
        /// Отмена
        /// </summary>
        public bool Cancel {  get; set; }
        /// <summary>
        /// Замена
        /// </summary>
        public bool Replacement { get; set; }
        /// <summary>
        /// Что меняем
        /// </summary>
        public int? fkTimetable { get; set; }
        [ForeignKey("fkTimetable")]
        public TimetableTable? Timetable { get; set; }

        /// <summary>
        /// На что меняем
        /// </summary>
        public int? fkSubject {  get; set; }
        /// <summary>
        /// На что меняем
        /// </summary>
        [ForeignKey(nameof(fkSubject))]
        public Subjects? Subjects { get; set; }
        /// <summary>
        /// Преподаватель
        /// </summary>
        public int? fkEmployee { get; set; }
        /// <summary>
        /// Преподаватель
        /// </summary>
        [ForeignKey("fkEmployee")]
        public EmployeesTable? Employees { get; set; }
    }
}
