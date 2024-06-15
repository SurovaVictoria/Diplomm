using Diplomm.Areas.Account.Models;
using Diplomm.Models;
using Diplomm.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Diplomm.Data
{
    public class AppDbContext : IdentityDbContext<EmployeesTable, ApplicationRole, int>
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<TimetableTable> TimetableTables => Set<TimetableTable>();
        public DbSet<EmployeesTable> EmployeesTables => Set<EmployeesTable>();
        public DbSet<OrganizationTable> OrganizationTables => Set<OrganizationTable>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<ChangesTable> ChangesTables => Set<ChangesTable>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=dipl;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
    }

}
