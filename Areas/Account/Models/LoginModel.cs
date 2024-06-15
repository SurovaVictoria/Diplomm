using Diplomm.Models.Tables;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplomm.Areas.Account.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Поле Логин обязательно для заполнения")]
        [DisplayName("Логин")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DisplayName("Пароль")]
        public string Password { get; set; }

    }
}

