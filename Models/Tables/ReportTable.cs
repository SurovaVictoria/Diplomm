using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomm.Models.Tables
{
    public class ReportTable 
    {
        [Key]
        public int ReportId { get; set; }
        //[DisplayName("Порядковый номер")]
        //public int? Number { get; set; }

        public DateTime DateTime { get; set; }

        //public int? fkEmployee {  get; set; }
        //[ForeignKey("fkEmployee")]
        public string Employee {  get; set; }

        [DisplayName("Тип")]
        public string Type {  get; set; }
        
        public int? fkLesson {  get; set; }
        [ForeignKey("fkLesson")]
        public Subjects? Lesson {  get; set; }

        public int? fkGroup { get; set; }
        [ForeignKey("fkGroup")]
        public Groups? Group { get; set; }

        public int? fkReplacement { get; set; }
        [ForeignKey("fkReplacement")]
        public EmployeesTable? Replacement { get; set; }

    }
}
