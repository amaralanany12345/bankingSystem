using banking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace banking.Configurations
{
    public class FinanceConfiguration : IEntityTypeConfiguration<Finance>
    {
        public void Configure(EntityTypeBuilder<Finance> builder)
        {
            builder.ToTable("finances").HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.FinanceNumber).IsRequired();
            builder.Property(a => a.FinanceValue).IsRequired();
            builder.Property(a => a.FinanceRepaymentValue).IsRequired();
            builder.Property(a => a.FinanceRequestTime).IsRequired();
            builder.Property(a => a.FinancePeriod).IsRequired();
            builder.Property(a => a.FinanceRepaymentValue).IsRequired();
            builder.Property(a => a.acceptFinance).IsRequired().HasDefaultValue(false);
            builder.Property(a => a.isFinanceRepaid).IsRequired().HasDefaultValue(false);
            builder.HasOne(a => a.customer).WithMany(a => a.Finances).HasForeignKey(a => a.customerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
