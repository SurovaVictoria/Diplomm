using Diplomm.Models.Tables;

namespace Diplomm.Areas.Account.Models
{
    public class RoleEdit
    {
        public ApplicationRole Role { get; set; }
        public IEnumerable<EmployeesTable> Members { get; set; }
        public IEnumerable<EmployeesTable> NonMembers { get; set; }
    }
}
