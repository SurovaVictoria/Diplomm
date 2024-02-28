using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplomm.Models
{
    public enum DayOfWeeks
    {
        [Display(Name = "Понедельник")]
        Monday = DayOfWeek.Monday,
        [Display(Name = "Вторник")]
        Tuesday = DayOfWeek.Tuesday,
        [Display(Name = "Среда")]
        Wednesday = DayOfWeek.Wednesday,
        [Display(Name = "Четверг")]
        Thursday = DayOfWeek.Thursday,
        [Display(Name = "Пятница")]
        Friday = DayOfWeek.Friday,
        [Display(Name = "Суббота")]
        Saturday = DayOfWeek.Saturday,
        [Display(Name = "Воскресенье")]
        Sunday = DayOfWeek.Sunday
    }
}
