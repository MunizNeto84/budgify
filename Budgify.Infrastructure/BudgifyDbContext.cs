using Budgify.Domain;
using Microsoft.EntityFrameworkCore;

namespace Budgify.Infrastructure
{
    public class BudgifyDbContext : DbContext
    {
        public BudgifyDbContext (DbContextOptions<BudgifyDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Income>()
                .Property(i => i.Category)
                .HasConversion<string>();

            modelBuilder.Entity<Expense>()
               .Property(e => e.Category)
               .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
