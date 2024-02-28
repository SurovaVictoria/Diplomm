using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplomm.Models
{
    public class Subjects
    {
        [Key]
        public int SubjectId {  get; set; }
        [DisplayName("Название предмета")]
        public string? SubjectName { get; set; }
    }
}
