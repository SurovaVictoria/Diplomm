using Diplomm.Models;
using Diplomm.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Diplomm.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AppDbContext()
        {
            // Не нужно удалять и создавать БД
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<TimetableTable> TimetableTables => Set<TimetableTable>();
        public DbSet<EmployeesTable> EmployeesTables => Set<EmployeesTable>();
        public DbSet<Groups> Groups => Set<Groups>();
        public DbSet<Subjects> Subjects => Set<Subjects>();
        public DbSet<ReportTable> ReportTables => Set<ReportTable>();
        public DbSet<ChangesTable> ChangesTables => Set<ChangesTable>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=bim;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }
    }

}
