using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplomm.Models
{
    public class Groups
    {
        [Key]
        public int GroupId { get; set; }
        [DisplayName("Название группы")]
        public string? GroupName { get; set; }
    }
}
