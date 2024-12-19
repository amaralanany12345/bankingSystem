using banking.Models;
using Microsoft.EntityFrameworkCore;

namespace banking
{
    public class AppDbContext:DbContext
    {
        public DbSet<Account> accounts { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Withdraw> withdraws { get; set; }
        public DbSet<Deposit> deposits { get; set; }
        public DbSet<Finance> finances{ get; set; }
        public DbSet<AnnualDeposit> annualDeposits { get; set; }
        public DbSet<AnnualDepositCashing> annualDepositCashing { get; set; }
        public DbSet<Transfer> transfers { get; set; }
        public DbSet<Employee> employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-NNDJ4G3D\SQLEXPRESS; Database =Banking; Integrated Security =SSPI; TrustServerCertificate =True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
