using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplomm.Models
{
    public enum DayOfWeeks
    {
        /// <summary>
        /// Понедельник
        /// </summary>
        [Display(Name = "Понедельник")] Monday = 1, // Установил Поненельник 1, чтоб соответствовало значениям библиотеки System.DayOfWeek
        /// <summary>
        /// Вторник
        /// </summary>
        [Display(Name = "Вторник")] Tuesday,
        /// <summary>
        /// Среда
        /// </summary>
        [Display(Name = "Среда")] Wednesday,
        /// <summary>
        /// Четверг
        /// </summary>
        [Display(Name = "Четверг")] Thursday,
        /// <summary>
        /// Пятница
        /// </summary>
        [Display(Name = "Пятница")] Friday,
        /// <summary>
        /// Суббота
        /// </summary>
        [Display(Name = "Суббота")] Saturday,
        /// <summary>
        /// Воскресенье
        /// </summary>
        [Display(Name = "Воскресенье")] Sunday
    }
}
