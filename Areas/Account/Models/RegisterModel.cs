using Diplomm.Models.Tables;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplomm.Areas.Account.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле Логин обязательно для заполнения")]
        [DisplayName("Логин")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле Фамилия обязательно для заполнения")]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Поле Отчество обязательно для заполнения")]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        public EmployeesTable GetUser()
        {
            EmployeesTable user = new()
            {
                UserName = Login,
                Name = Name,
                Surname = Surname,
                Patronymic = Patronymic
            };
            return user;
        }
    }
}

