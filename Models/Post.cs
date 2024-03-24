using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplomm.Models
{
    public class Post
    {
        [Key]
        public int PostId {  get; set; }
        [DisplayName("Название должности")]
        public string? PostName { get; set; }
    }
}
