using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomm.Models.Tables
{
    public class TimetableTable 
    {
        [Key]
        public int TimetableID { get; set; }
        [DisplayName("День недели")]
        public DayOfWeeks DayOfWeek { get; set; }
        [DisplayName("Номер урока")]
        public int Number { get; set; }

        [DisplayName("Предмет")]
        public int? fkSubjects { get; set; }
        [ForeignKey("fkSubjects")]
        public Subjects? Subject { get; set; }

        [DisplayName("Группа")]
        public int? fkGroups { get; set; }
        [ForeignKey("fkGroups")]
        public Groups? Group { get; set; }

        public int? fkEmployees { get; set; }
        [ForeignKey("fkEmployees")]
        public EmployeesTable? Employee { get; set; }

    }

}
