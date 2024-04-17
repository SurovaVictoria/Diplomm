using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplomm.Models.Tables
{
    public class OrganizationTable
    {
        [Key]
        public int ShopId { get; set; }
        /// <summary>
        /// Название магазина
        /// </summary>
        [DisplayName("Группа")]
        public string? ShopName { get; set; }
        /// <summary>
        /// Адрес магазина
        /// </summary>
        [DisplayName("Специальность")]
        public string? Address { get; set; }
    }
}
