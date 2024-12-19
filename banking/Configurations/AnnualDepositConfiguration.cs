using banking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace banking.Configurations
{
    public class AnnualDepositConfiguration : IEntityTypeConfiguration<AnnualDeposit>
    {
        public void Configure(EntityTypeBuilder<AnnualDeposit> builder)
        {
            builder.ToTable("AnnualDeposits").HasKey(a => a.id);
            builder.Property(a => a.id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.annualDepositNumber).IsRequired();
            builder.Property(a => a.AnnualDepositValue).IsRequired();
            builder.Property(a => a.AnnualDepositRepaymentValue).IsRequired();
            builder.Property(a => a.AnnualDepositRequestTime).IsRequired();
            builder.Property(a => a.AnnualDepositPeriod).IsRequired();
            builder.Property(a => a.AnnualDepositRepaymentValue).IsRequired();
            builder.Property(a => a.acceptAnnualDeposit).IsRequired().HasDefaultValue(false);
            builder.Property(a => a.annualDepositType).IsRequired();
            builder.Property(a => a.allowableCashingFromAnnualDeposit).IsRequired();
            builder.HasOne(a => a.customer).WithMany(a => a.annualDeposits).HasForeignKey(a => a.customerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
