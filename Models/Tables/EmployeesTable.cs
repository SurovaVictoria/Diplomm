using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplomm.Models.Tables
{
    public class EmployeesTable : IdentityUser<int>
    {
        [Key]
        public int EmployeesId { get; set; }
        [DisplayName("Имя")]
        public string? Name { get; set; }
        [DisplayName("Фамилия")]
        public string? Surname { get; set; }
        [DisplayName("Отчество")]
        public string? Patronymic { get; set; }
        [DisplayName("ФИО")]
        public string? FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", Name, Patronymic, Surname);
            }
        }
    }
}
