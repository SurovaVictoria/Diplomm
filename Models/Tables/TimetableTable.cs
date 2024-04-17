using Diplomm.Data;
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
        [DisplayName("Порядковый номер")]
        public int Number { get; set; }

        [DisplayName("Предмет")]
        public int? fkPosts { get; set; }
        [ForeignKey("fkPosts")]
        public Post? Post { get; set; }

        [DisplayName("Группа")]
        public int? fkOrganizations { get; set; }
        [ForeignKey("fkOrganizations")]
        public OrganizationTable? Organization { get; set; }

        public int? fkEmployees { get; set; }
        [ForeignKey("fkEmployees")]
        public EmployeesTable? Employee { get; set; }

        public string? GetName {
            get
            {
                string post = Post == null ? "" : Post.PostName ?? "-";
                string nameOfWeek = (DayOfWeek.GetType().GetField(DayOfWeek.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[])[0].Name;
                return $"{nameOfWeek} {Number} {post}";
            }
        }

    }

}
