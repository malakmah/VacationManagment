

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VacationManagement.Models;

namespace VacationManagement.Data
{
    public class ApplicationDbContext:IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<sqlGetReports>().HasNoKey().ToSqlQuery("sqlGetReports");


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Login>().HasNoKey().ToSqlQuery("Logins");
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<RequestVacation> RequestVacations { get; set; }
        public DbSet<VacationPlan> VacationPlans { get; set; }
        public DbSet<sqlGetReports> sqlGetReports { get; set; }
        //public DbSet<RegisterLogin> RegisterLogins { get; set; }
        //public DbSet<Login> Logins { get; set; }


    }
}
